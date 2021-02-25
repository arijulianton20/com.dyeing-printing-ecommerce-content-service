using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.dyeingprinting.service.content.data.Migrations
{
    public partial class AddCustomerCareTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCare",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    ImageUrl1 = table.Column<string>(nullable: true),
                    ContentTitle1 = table.Column<string>(nullable: true),
                    ContentDescription1 = table.Column<string>(nullable: true),
                    TextButton1 = table.Column<string>(nullable: true),
                    ContentUrl1 = table.Column<string>(nullable: true),
                    ImageUrl2 = table.Column<string>(nullable: true),
                    ContentTitle2 = table.Column<string>(nullable: true),
                    ContentDescription2 = table.Column<string>(nullable: true),
                    TextButton2 = table.Column<string>(nullable: true),
                    ContentUrl2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCare", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCare");
        }
    }
}
