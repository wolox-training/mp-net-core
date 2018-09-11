using Microsoft.EntityFrameworkCore.Migrations;

namespace training_net.Migrations
{
    public partial class RenameCommentText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "Comment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CommentString",
                table: "Comment",
                newName: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comment",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comment",
                newName: "CommentString");
        }
    }
}
