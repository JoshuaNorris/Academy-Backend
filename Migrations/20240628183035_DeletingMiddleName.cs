using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademyApi.Migrations
{
    /// <inheritdoc />
    public partial class DeletingMiddleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Authors",
                type: "varchar",
                maxLength: 30,
                nullable: true);
        }
    }
}
