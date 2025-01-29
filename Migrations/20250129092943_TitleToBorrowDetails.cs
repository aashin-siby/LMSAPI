using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAPI.Migrations
{
    /// <inheritdoc />
    public partial class TitleToBorrowDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BorrowDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "BorrowDetails");
        }
    }
}
