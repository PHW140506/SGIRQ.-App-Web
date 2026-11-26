using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Migrations;
[DbContext(typeof(SgirqDbContext))]
[Migration("20261121190000_SeguridadInventario")]
public sealed class SeguridadInventario : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(name: "IX_Reactivos_nombreComercial", table: "Reactivos", column: "nombreComercial", unique: true);
        migrationBuilder.CreateIndex(name: "IX_Reactivos_nombreQuimico", table: "Reactivos", column: "nombreQuimico", unique: true);
        migrationBuilder.Sql("UPDATE Personal SET tipoPersonal = 'Auxiliar' WHERE tipoPersonal = 'Solicitante'");
        migrationBuilder.AlterColumn<string>(name: "tipoPersonal", table: "Personal", type: "enum('Administrador','Auxiliar')", nullable: false, oldClrType: typeof(string), oldType: "enum('Administrador','Auxiliar','Solicitante')");
        migrationBuilder.CreateTable(name: "Entrada", columns: table => new
        {
            idEntrada = table.Column<int>(type: "int", nullable: false).Annotation("MySql:ValueGenerationStrategy", Microsoft.EntityFrameworkCore.Metadata.MySqlValueGenerationStrategy.IdentityColumn),
            idReactivo = table.Column<int>(type: "int", nullable: false),
            cantidad = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
            fechaEntrada = table.Column<DateTime>(type: "datetime", nullable: false)
        }, constraints: table => { table.PrimaryKey("PK_Entrada", x => x.idEntrada); table.ForeignKey("FK_Entrada_Reactivos_idReactivo", x => x.idReactivo, "Reactivos", "idReactivo", onDelete: ReferentialAction.Restrict); });
        migrationBuilder.CreateIndex(name: "IX_Entrada_idReactivo", table: "Entrada", column: "idReactivo");
        migrationBuilder.CreateIndex(name: "IX_Entrada_fechaEntrada", table: "Entrada", column: "fechaEntrada");
        migrationBuilder.Sql("ALTER TABLE Entrada ADD CONSTRAINT CK_Entrada_Cantidad CHECK (cantidad > 0)");
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Entrada");
        migrationBuilder.DropIndex(name: "IX_Reactivos_nombreComercial", table: "Reactivos");
        migrationBuilder.DropIndex(name: "IX_Reactivos_nombreQuimico", table: "Reactivos");
        migrationBuilder.AlterColumn<string>(name: "tipoPersonal", table: "Personal", type: "enum('Administrador','Auxiliar','Solicitante')", nullable: false, oldClrType: typeof(string), oldType: "enum('Administrador','Auxiliar')");
    }
}
