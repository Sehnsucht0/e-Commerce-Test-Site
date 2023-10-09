using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_Commerce_Test_Site.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserData_UserDataId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserData_AspNetUsers_UserId",
                table: "UserData");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserData_UserDataId",
                table: "Orders",
                column: "UserDataId",
                principalTable: "UserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_AspNetUsers_UserId",
                table: "UserData",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserData_UserDataId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserData_AspNetUsers_UserId",
                table: "UserData");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserData_UserDataId",
                table: "Orders",
                column: "UserDataId",
                principalTable: "UserData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_AspNetUsers_UserId",
                table: "UserData",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
