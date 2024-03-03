using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PEP.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "algorithm_problems",
                columns: table => new
                {
                    problem_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    accept_rate = table.Column<int>(type: "int", nullable: true),
                    difficulty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    test_amount = table.Column<int>(type: "int", nullable: true),
                    problem_content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__algorith__69B87CECB40E0363", x => x.problem_id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    chapter_count = table.Column<int>(type: "int", nullable: false),
                    introduction = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__courses__2AA84FD131A1069E", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    user_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    account = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    avatar = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__CB9A1CFF97EC8EA4", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "problem_tags",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    problem_id = table.Column<int>(type: "int", nullable: true),
                    tag_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tag_color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__problem___4296A2B61260FB1B", x => x.tag_id);
                    table.ForeignKey(
                        name: "fk_problem_tags_problem_id",
                        column: x => x.problem_id,
                        principalTable: "algorithm_problems",
                        principalColumn: "problem_id");
                });

            migrationBuilder.CreateTable(
                name: "test_data",
                columns: table => new
                {
                    test_data_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    problem_id = table.Column<int>(type: "int", nullable: true),
                    sequence_number = table.Column<int>(type: "int", nullable: true),
                    input_data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    output_data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__test_dat__C3AF6FF27FC55BE2", x => x.test_data_id);
                    table.ForeignKey(
                        name: "fk_test_data_problem_id",
                        column: x => x.problem_id,
                        principalTable: "algorithm_problems",
                        principalColumn: "problem_id");
                });

            migrationBuilder.CreateTable(
                name: "course_chapters",
                columns: table => new
                {
                    chapter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    chapter_number = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__course_c__745EFE87783BE697", x => x.chapter_id);
                    table.ForeignKey(
                        name: "fk_course_chapters_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id");
                });

            migrationBuilder.CreateTable(
                name: "course_tags",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    tag_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    tag_color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__course_t__50FC01572FB805B7", x => x.tagId);
                    table.ForeignKey(
                        name: "fk_tag_course",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id");
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    post_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    user_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    post_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    post_content = table.Column<string>(type: "text", nullable: false),
                    post_type = table.Column<bool>(type: "bit", nullable: false),
                    avatar_imgurl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    problem_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__posts__3ED78766341668CE", x => x.post_id);
                    table.ForeignKey(
                        name: "fk_posts_algorithm_problems",
                        column: x => x.problem_id,
                        principalTable: "algorithm_problems",
                        principalColumn: "problem_id");
                    table.ForeignKey(
                        name: "fk_posts_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "submission_records",
                columns: table => new
                {
                    record_id = table.Column<int>(type: "int", nullable: false),
                    problem_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    user_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    result_state = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    compiler = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    memory = table.Column<int>(type: "int", nullable: true),
                    runtime = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    submit_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    compiler_output = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__records__BFCFB4DD8379D7F1", x => x.record_id);
                    table.ForeignKey(
                        name: "FK__records__problem__625A9A57",
                        column: x => x.problem_id,
                        principalTable: "algorithm_problems",
                        principalColumn: "problem_id");
                    table.ForeignKey(
                        name: "FK__records__user_id__634EBE90",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_courses",
                columns: table => new
                {
                    user_course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    course_id = table.Column<int>(type: "int", nullable: true),
                    is_favorite = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_cou__651E082692E5E6C8", x => x.user_course_id);
                    table.ForeignKey(
                        name: "fk_user_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "fk_user_courses_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "sub_chapters",
                columns: table => new
                {
                    sub_chapter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: true),
                    parent_chapter_id = table.Column<int>(type: "int", nullable: true),
                    parent_chapter_number = table.Column<int>(type: "int", nullable: true),
                    sub_chapter_number = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    markdown_content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sub_chap__2B141C0DACD75C26", x => x.sub_chapter_id);
                    table.ForeignKey(
                        name: "fk_sub_chapters_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "fk_sub_chapters_parent_chapter_id",
                        column: x => x.parent_chapter_id,
                        principalTable: "course_chapters",
                        principalColumn: "chapter_id");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    post_id = table.Column<int>(type: "int", nullable: false),
                    from_user_id = table.Column<int>(type: "int", nullable: false),
                    from_username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    comment_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    avatar_imgurl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comments__E795768775FC3BBB", x => x.comment_id);
                    table.ForeignKey(
                        name: "fk_comment_to_user_id",
                        column: x => x.from_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_comments_post_id",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "post_id");
                });

            migrationBuilder.CreateTable(
                name: "replies",
                columns: table => new
                {
                    reply_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment_id = table.Column<int>(type: "int", nullable: false),
                    from_user_id = table.Column<int>(type: "int", nullable: false),
                    from_username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    to_user_id = table.Column<int>(type: "int", nullable: false),
                    to_username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    reply_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    avatar_imgurl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__replies__EE40569834EB4973", x => x.reply_id);
                    table.ForeignKey(
                        name: "fk_replies_comment_id",
                        column: x => x.comment_id,
                        principalTable: "comments",
                        principalColumn: "comment_id");
                    table.ForeignKey(
                        name: "fk_replies_from_user_id",
                        column: x => x.from_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_replies_to_user_id",
                        column: x => x.to_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_from_user_id",
                table: "comments",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_post_id",
                table: "comments",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_chapters_course_id",
                table: "course_chapters",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_tags_course_id",
                table: "course_tags",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_problem_id",
                table: "posts",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_user_id",
                table: "posts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_problem_tags_problem_id",
                table: "problem_tags",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "IX_replies_comment_id",
                table: "replies",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_replies_from_user_id",
                table: "replies",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_replies_to_user_id",
                table: "replies",
                column: "to_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_chapters_course_id",
                table: "sub_chapters",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_chapters_parent_chapter_id",
                table: "sub_chapters",
                column: "parent_chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_submission_records_problem_id",
                table: "submission_records",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "IX_submission_records_user_id",
                table: "submission_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_data_problem_id",
                table: "test_data",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_courses_course_id",
                table: "user_courses",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_courses_user_id",
                table: "user_courses",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_tags");

            migrationBuilder.DropTable(
                name: "problem_tags");

            migrationBuilder.DropTable(
                name: "replies");

            migrationBuilder.DropTable(
                name: "sub_chapters");

            migrationBuilder.DropTable(
                name: "submission_records");

            migrationBuilder.DropTable(
                name: "test_data");

            migrationBuilder.DropTable(
                name: "user_courses");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "course_chapters");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "algorithm_problems");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
