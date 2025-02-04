using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedBorrowDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowDetails_Users_UserId",
                table: "BorrowDetails");

            migrationBuilder.DropIndex(
                name: "IX_BorrowDetails_UserId",
                table: "BorrowDetails");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "BorrowDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "BorrowDetails");

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
    }
}
