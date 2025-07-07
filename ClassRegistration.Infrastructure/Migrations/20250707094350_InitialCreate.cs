using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: false),
                    IsClassFull = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "ClassStudent",
                columns: table => new
                {
                    EnrolledClassesClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrolledStudentsStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStudent", x => new { x.EnrolledClassesClassID, x.EnrolledStudentsStudentId });
                    table.ForeignKey(
                        name: "FK_ClassStudent_Classes_EnrolledClassesClassID",
                        column: x => x.EnrolledClassesClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassStudent_Students_EnrolledStudentsStudentId",
                        column: x => x.EnrolledStudentsStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudent_EnrolledStudentsStudentId",
                table: "ClassStudent",
                column: "EnrolledStudentsStudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassStudent");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
