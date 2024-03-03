using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PEP.Migrations
{
    /// <inheritdoc />
    public partial class version11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_courses_course_id",
                table: "user_courses");

            migrationBuilder.DropForeignKey(
                name: "fk_user_courses_user_id",
                table: "user_courses");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "user_courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "course_id",
                table: "user_courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_user_courses_course_id",
                table: "user_courses",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "course_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_courses_user_id",
                table: "user_courses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_courses_course_id",
                table: "user_courses");

            migrationBuilder.DropForeignKey(
                name: "fk_user_courses_user_id",
                table: "user_courses");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "user_courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "course_id",
                table: "user_courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_user_courses_course_id",
                table: "user_courses",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "course_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_courses_user_id",
                table: "user_courses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id");
        }
    }
}
