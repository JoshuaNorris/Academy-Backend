using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AcademyApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingIDFieldtoBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToBooks_Books_BookTitle",
                table: "UserToBooks");

            migrationBuilder.DropIndex(
                name: "IX_UserToBooks_BookTitle",
                table: "UserToBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "UserToBooks");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "UserToBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserToBooks_BookId",
                table: "UserToBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToBooks_Books_BookId",
                table: "UserToBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToBooks_Books_BookId",
                table: "UserToBooks");

            migrationBuilder.DropIndex(
                name: "IX_UserToBooks_BookId",
                table: "UserToBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "UserToBooks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
                table: "UserToBooks",
                type: "varchar",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_UserToBooks_BookTitle",
                table: "UserToBooks",
                column: "BookTitle");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToBooks_Books_BookTitle",
                table: "UserToBooks",
                column: "BookTitle",
                principalTable: "Books",
                principalColumn: "Title",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
