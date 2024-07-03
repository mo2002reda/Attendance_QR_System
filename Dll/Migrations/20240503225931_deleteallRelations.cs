using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dll.Migrations
{
    public partial class deleteallRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjects_doctors_DoctorsId",
                table: "subjects");

            migrationBuilder.DropTable(
                name: "AttendanceTableStudent");

            migrationBuilder.DropTable(
                name: "AttendanceTableSubject");

            migrationBuilder.DropTable(
                name: "DoctorStudent");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropIndex(
                name: "IX_subjects_DoctorsId",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "DoctorsId",
                table: "subjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorsId",
                table: "subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttendanceTableStudent",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    StudentIdid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceTableStudent", x => new { x.AttendanceId, x.StudentIdid });
                    table.ForeignKey(
                        name: "FK_AttendanceTableStudent_attendanceTables_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "attendanceTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceTableStudent_students_StudentIdid",
                        column: x => x.StudentIdid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceTableSubject",
                columns: table => new
                {
                    attendanceTablesId = table.Column<int>(type: "int", nullable: false),
                    subjectsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceTableSubject", x => new { x.attendanceTablesId, x.subjectsid });
                    table.ForeignKey(
                        name: "FK_AttendanceTableSubject_attendanceTables_attendanceTablesId",
                        column: x => x.attendanceTablesId,
                        principalTable: "attendanceTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceTableSubject_subjects_subjectsid",
                        column: x => x.subjectsid,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorStudent",
                columns: table => new
                {
                    DoctorsId = table.Column<int>(type: "int", nullable: false),
                    Studentsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStudent", x => new { x.DoctorsId, x.Studentsid });
                    table.ForeignKey(
                        name: "FK_DoctorStudent_doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorStudent_students_Studentsid",
                        column: x => x.Studentsid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    studentsid = table.Column<int>(type: "int", nullable: false),
                    subjectsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => new { x.studentsid, x.subjectsid });
                    table.ForeignKey(
                        name: "FK_StudentSubject_students_studentsid",
                        column: x => x.studentsid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_subjects_subjectsid",
                        column: x => x.subjectsid,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subjects_DoctorsId",
                table: "subjects",
                column: "DoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceTableStudent_StudentIdid",
                table: "AttendanceTableStudent",
                column: "StudentIdid");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceTableSubject_subjectsid",
                table: "AttendanceTableSubject",
                column: "subjectsid");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorStudent_Studentsid",
                table: "DoctorStudent",
                column: "Studentsid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_subjectsid",
                table: "StudentSubject",
                column: "subjectsid");

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_doctors_DoctorsId",
                table: "subjects",
                column: "DoctorsId",
                principalTable: "doctors",
                principalColumn: "Id");
        }
    }
}
