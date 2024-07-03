using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dll.Migrations
{
    public partial class addSubjectnameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subjectName",
                table: "attendanceTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subjectName",
                table: "attendanceTables");
        }
    }
}
