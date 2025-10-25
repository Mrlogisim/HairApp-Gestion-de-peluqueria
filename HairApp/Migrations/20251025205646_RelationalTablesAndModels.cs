using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HairApp.Migrations
{
    /// <inheritdoc />
    public partial class RelationalTablesAndModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "Id_cliente");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Peluqueros",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Clientes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    id_insumo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    unidad_medida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    stock_minimo = table.Column<int>(type: "integer", nullable: false),
                    fecha_ultima_reposicion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.id_insumo);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id_permiso = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id_permiso);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id_servicio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Duracion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id_servicio);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id_Turno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Hora_Inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Hora_Fin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id_Turno);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                columns: table => new
                {
                    Id_Rol = table.Column<int>(type: "integer", nullable: false),
                    Id_Permiso = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => new { x.Id_Rol, x.Id_Permiso });
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos_Id_Permiso",
                        column: x => x.Id_Permiso,
                        principalTable: "Permisos",
                        principalColumn: "Id_permiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles_Id_Rol",
                        column: x => x.Id_Rol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    IdRol = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeluqueroServicios",
                columns: table => new
                {
                    PeluqueroId = table.Column<int>(type: "integer", nullable: false),
                    ServicioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeluqueroServicios", x => new { x.PeluqueroId, x.ServicioId });
                    table.ForeignKey(
                        name: "FK_PeluqueroServicios_Peluqueros_PeluqueroId",
                        column: x => x.PeluqueroId,
                        principalTable: "Peluqueros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeluqueroServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id_servicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicioInsumo",
                columns: table => new
                {
                    Id_Servicio = table.Column<int>(type: "integer", nullable: false),
                    Id_Insumo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioInsumo", x => new { x.Id_Servicio, x.Id_Insumo });
                    table.ForeignKey(
                        name: "FK_ServicioInsumo_Insumo_Id_Insumo",
                        column: x => x.Id_Insumo,
                        principalTable: "Insumo",
                        principalColumn: "id_insumo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioInsumo_Servicios_Id_Servicio",
                        column: x => x.Id_Servicio,
                        principalTable: "Servicios",
                        principalColumn: "Id_servicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurnoClientes",
                columns: table => new
                {
                    Id_cliente = table.Column<int>(type: "integer", nullable: false),
                    Id_turno = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoClientes", x => new { x.Id_cliente, x.Id_turno });
                    table.ForeignKey(
                        name: "FK_TurnoClientes_Clientes_Id_cliente",
                        column: x => x.Id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnoClientes_Turnos_Id_turno",
                        column: x => x.Id_turno,
                        principalTable: "Turnos",
                        principalColumn: "Id_Turno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurnoDetalles",
                columns: table => new
                {
                    Id_Turno = table.Column<int>(type: "integer", nullable: false),
                    Id_Cliente = table.Column<int>(type: "integer", nullable: false),
                    Id_Peluquero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoDetalles", x => new { x.Id_Turno, x.Id_Cliente, x.Id_Peluquero });
                    table.ForeignKey(
                        name: "FK_TurnoDetalles_Clientes_Id_Cliente",
                        column: x => x.Id_Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnoDetalles_Peluqueros_Id_Peluquero",
                        column: x => x.Id_Peluquero,
                        principalTable: "Peluqueros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnoDetalles_Turnos_Id_Turno",
                        column: x => x.Id_Turno,
                        principalTable: "Turnos",
                        principalColumn: "Id_Turno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peluqueros_UsuarioId",
                table: "Peluqueros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PeluqueroServicios_ServicioId",
                table: "PeluqueroServicios",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_Id_Permiso",
                table: "RolPermisos",
                column: "Id_Permiso");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioInsumo_Id_Insumo",
                table: "ServicioInsumo",
                column: "Id_Insumo");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoClientes_Id_turno",
                table: "TurnoClientes",
                column: "Id_turno");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoDetalles_Id_Cliente",
                table: "TurnoDetalles",
                column: "Id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoDetalles_Id_Peluquero",
                table: "TurnoDetalles",
                column: "Id_Peluquero");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioId",
                table: "Clientes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Peluqueros_Usuarios_UsuarioId",
                table: "Peluqueros",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Peluqueros_Usuarios_UsuarioId",
                table: "Peluqueros");

            migrationBuilder.DropTable(
                name: "PeluqueroServicios");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "ServicioInsumo");

            migrationBuilder.DropTable(
                name: "TurnoClientes");

            migrationBuilder.DropTable(
                name: "TurnoDetalles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Insumo");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Peluqueros_UsuarioId",
                table: "Peluqueros");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Peluqueros");

            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Id_cliente",
                table: "Clientes",
                newName: "Id");
        }
    }
}
