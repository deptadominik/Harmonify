using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmonify.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationForFriends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_ApplicationUserId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_MainUserId",
                table: "Friendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendship_ApplicationUserId",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Friendship");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_MainUserId",
                table: "Friendship",
                column: "MainUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_MainUserId",
                table: "Friendship");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Friendship",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_ApplicationUserId",
                table: "Friendship",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_ApplicationUserId",
                table: "Friendship",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_MainUserId",
                table: "Friendship",
                column: "MainUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
