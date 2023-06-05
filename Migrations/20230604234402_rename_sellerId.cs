using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsdsShop.Migrations
{
    public partial class rename_sellerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Sellers",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sellers",
                newName: "SellerId");
        }
    }
}
