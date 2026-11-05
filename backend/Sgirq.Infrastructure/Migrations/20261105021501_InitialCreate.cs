using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sgirq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    idPersonal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombrePersonal = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellidosPersonal = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuarioPersonalLogin = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contrasenia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipoPersonal = table.Column<string>(type: "enum('Administrador','Auxiliar','Solicitante')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.idPersonal);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idUsuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reactivos",
                columns: table => new
                {
                    idReactivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombreComercial = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombreQuimico = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numeroCAS = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estadoFisico = table.Column<string>(type: "enum('Líquido','Sólido')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cantidadReactivo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    unidadMedida = table.Column<string>(type: "enum('Mililitro','Gramo','Litro','Kilogramo')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stockMinimo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    colorAnaquel = table.Column<string>(type: "enum('Verde','Rojo','Azul','Amarillo','Blanco','Naranja')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idPersonal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactivos", x => x.idReactivo);
                    table.CheckConstraint("CK_Reactivos_Cantidad", "cantidadReactivo >= 0 AND stockMinimo >= 0");
                    table.ForeignKey(
                        name: "FK_Reactivos_Personal_idPersonal",
                        column: x => x.idPersonal,
                        principalTable: "Personal",
                        principalColumn: "idPersonal",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reporte",
                columns: table => new
                {
                    idReporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    fechaFinal = table.Column<DateTime>(type: "date", nullable: false),
                    tipoReporte = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idPersonal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporte", x => x.idReporte);
                    table.ForeignKey(
                        name: "FK_Reporte_Personal_idPersonal",
                        column: x => x.idPersonal,
                        principalTable: "Personal",
                        principalColumn: "idPersonal",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Salida",
                columns: table => new
                {
                    idSalida = table.Column<int>(type: "int", nullable: false),
                    fechaSalida = table.Column<DateTime>(type: "datetime", nullable: false),
                    motivoSalida = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idPersonal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salida", x => x.idSalida);
                    table.ForeignKey(
                        name: "FK_Salida_Personal_idPersonal",
                        column: x => x.idPersonal,
                        principalTable: "Personal",
                        principalColumn: "idPersonal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salida_Usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Requiere",
                columns: table => new
                {
                    idSalida = table.Column<int>(type: "int", nullable: false),
                    idReactivo = table.Column<int>(type: "int", nullable: false),
                    cantidadSolicitada = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requiere", x => new { x.idSalida, x.idReactivo });
                    table.CheckConstraint("CK_Requiere_Cantidad", "cantidadSolicitada > 0");
                    table.ForeignKey(
                        name: "FK_Requiere_Reactivos_idReactivo",
                        column: x => x.idReactivo,
                        principalTable: "Reactivos",
                        principalColumn: "idReactivo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requiere_Salida_idSalida",
                        column: x => x.idSalida,
                        principalTable: "Salida",
                        principalColumn: "idSalida",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_usuarioPersonalLogin",
                table: "Personal",
                column: "usuarioPersonalLogin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactivos_idPersonal",
                table: "Reactivos",
                column: "idPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Reactivos_numeroCAS",
                table: "Reactivos",
                column: "numeroCAS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reporte_idPersonal",
                table: "Reporte",
                column: "idPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Requiere_idReactivo",
                table: "Requiere",
                column: "idReactivo");

            migrationBuilder.CreateIndex(
                name: "IX_Salida_fechaSalida",
                table: "Salida",
                column: "fechaSalida");

            migrationBuilder.CreateIndex(
                name: "IX_Salida_idPersonal",
                table: "Salida",
                column: "idPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Salida_idUsuario",
                table: "Salida",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reporte");

            migrationBuilder.DropTable(
                name: "Requiere");

            migrationBuilder.DropTable(
                name: "Reactivos");

            migrationBuilder.DropTable(
                name: "Salida");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
