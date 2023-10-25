using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ahu.DataAccess.Migrations
{
    public partial class ProductReviewTableAddedTwoColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "ProductReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "ProductReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ProductReviews");
        }
    }
}
