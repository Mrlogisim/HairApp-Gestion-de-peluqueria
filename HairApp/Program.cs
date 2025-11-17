using HairApp.Data;
using HairApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


// NUEVO: Implementación de autenticación obligatoria
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie(options => // Este método configura el esquema de autenticación basado en cookies.
//                      // Aquí defines cómo se manejarán las cookies de autenticación.
//{
//    // LoginPath: Especifica la ruta a la que se redirigirá al usuario cuando intente acceder a una página protegida sin estar autenticado.
//    // LogoutPath: Define la ruta que se utilizará para cerrar sesión. 
//    options.LoginPath = "/Identity/Account/Login"; 
//    options.LogoutPath = "/Identity/Account/Logout";
//});


// Con esto puedo ver los cambios con solo actualizar la pagina
builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); 

// Configurar EF Core con PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Identity (usuarios, roles, etc.)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // No requiere email confirmado 
})
.AddRoles<IdentityRole>() // (Opcional) Si vas a usar roles
.AddEntityFrameworkStores<ApplicationDbContext>();

// Agregar soporte para controladores con vistas
builder.Services.AddControllersWithViews();

// Aca estoy registrando los servicios en un contenedor de dependencias de ASP.NET Core (usando builder.Services)
// AddScoped define el ciclo de vida de los servicios.
// Con esto puedo usar los servicios en los controladores(?) 
builder.Services.AddScoped<InsumoService>();
builder.Services.AddScoped<ClienteService>();

// Para que primero aparezca el login. Esto es de Identity.Application (?)
// Antes esto estaba como comentario y el que se encargaba del login
// era el builder.Services.AddAuthentication(options =>
builder.Services.ConfigureApplicationCookie(options =>
{
    // Ruta a donde se redirige si el usuario no está autenticado
    options.LoginPath = "/Identity/Account/Login";

    // Ruta a donde se redirige si no tiene permisos (por ejemplo, rol incorrecto)
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

    options.Cookie.Name = "HairAppAuth";

    // (Opcional) Tiempo de expiración de la cookie
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    // (Opcional) Redirige automáticamente si la sesión expira
    options.SlidingExpiration = true;
});




var app = builder.Build();

// Middleware de la app
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar autenticaci�n y autorizacion
app.UseAuthentication();
app.UseAuthorization();


/*
app.MapGet("/", context =>
{
    context.Response.Redirect("/Identity/Account/Login");
    return Task.CompletedTask;
});
*/


/*
app.MapGet("/", context =>
{
    if (context.User?.Identity?.IsAuthenticated ?? false)
    {
        context.Response.Redirect("/Home/Index");
    }
    else
    {
        context.Response.Redirect("/Identity/Account/Login");
    }
    return Task.CompletedTask;
});
*/


// Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// (Opcional, si usas Razor Pages para Identity)
app.MapRazorPages();

app.Run();

