using HairApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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

/*
builder.Services.ConfigureApplicationCookie(options =>
{
    // Ruta a donde se redirige si el usuario no está autenticado
    options.LoginPath = "/Identity/Account/Login";

    // Ruta a donde se redirige si no tiene permisos (por ejemplo, rol incorrecto)
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

    // (Opcional) Tiempo de expiración de la cookie
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    // (Opcional) Redirige automáticamente si la sesión expira
    options.SlidingExpiration = true;
});
*/



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

