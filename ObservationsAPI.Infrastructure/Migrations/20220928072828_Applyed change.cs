using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObservationsAPI.Infrastructure.Migrations
{
    public partial class Applyedchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Applyed",
                table: "Controles",
                newName: "Aplicado");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Controles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Aplicado",
                table: "Controles",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Aplicado",
                table: "Controles",
                newName: "Applyed");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Controles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "Applyed",
                table: "Controles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
