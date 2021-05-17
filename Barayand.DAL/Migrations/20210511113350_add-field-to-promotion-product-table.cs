using Microsoft.EntityFrameworkCore.Migrations;

namespace Barayand.DAL.Migrations
{
    public partial class addfieldtopromotionproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "X_ProdTitle",
                table: "PromotionBoxProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X_ProdTitle",
                table: "PromotionBoxProducts");
        }
    }
}
