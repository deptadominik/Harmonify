using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmonify.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatar_User_UserId",
                table: "Avatar");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatar_User_UserId",
                table: "Avatar",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatar_User_UserId",
                table: "Avatar");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatar_User_UserId",
                table: "Avatar",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
