using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BorrowDetails_UserId",
                table: "BorrowDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowDetails_Users_UserId",
                table: "BorrowDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowDetails_Users_UserId",
                table: "BorrowDetails");

            migrationBuilder.DropIndex(
                name: "IX_BorrowDetails_UserId",
                table: "BorrowDetails");
        }
    }
}
