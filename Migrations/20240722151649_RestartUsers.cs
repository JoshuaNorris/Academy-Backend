using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademyApi.Migrations
{
    /// <inheritdoc />
    public partial class RestartUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "char(60)", maxLength: 60, nullable: false),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "varchar", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
