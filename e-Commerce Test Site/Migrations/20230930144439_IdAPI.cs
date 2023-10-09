using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_Commerce_Test_Site.Migrations
{
    /// <inheritdoc />
    public partial class IdAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAPI",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAPI",
                table: "Products");
        }
    }
}
