using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AcademyApi.Migrations
{
    /// <inheritdoc />
    public partial class RestartingDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "varchar", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    HashedPassword = table.Column<string>(type: "char(60)", maxLength: 60, nullable: false),
                    UserRole = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Title = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Title);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(type: "varchar", nullable: false),
                    BookTitle = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToBooks_Books_BookTitle",
                        column: x => x.BookTitle,
                        principalTable: "Books",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToBooks_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToBooks_BookTitle",
                table: "UserToBooks",
                column: "BookTitle");

            migrationBuilder.CreateIndex(
                name: "IX_UserToBooks_UserEmail",
                table: "UserToBooks",
                column: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
