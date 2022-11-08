using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgressNotesAPI.Infrastructure.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObservationId",
                table: "Seguimientos",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservationId",
                table: "Seguimientos");
        }
    }
}
