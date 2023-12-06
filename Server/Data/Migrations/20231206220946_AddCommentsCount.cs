using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmonify.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentsCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentsCount",
                table: "Post",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsCount",
                table: "Post");
        }
    }
}
