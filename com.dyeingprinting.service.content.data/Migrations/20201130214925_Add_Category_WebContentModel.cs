using Microsoft.EntityFrameworkCore.Migrations;

namespace com.dyeingprinting.service.content.data.Migrations
{
    public partial class Add_Category_WebContentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MobileContents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "MobileContents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MobileContents");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "MobileContents");
        }
    }
}
