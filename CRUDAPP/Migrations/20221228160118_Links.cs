using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDAPP.Migrations
{
    /// <inheritdoc />
    public partial class Links : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Movies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Directors",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Directors");
        }
    }
}
