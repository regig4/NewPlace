using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "payment",
                table: "User",
                newName: "id");

            migrationBuilder.AddColumn<decimal>(
                name: "points_amount",
                schema: "payment",
                table: "User",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "points_amount",
                schema: "payment",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "payment",
                table: "User",
                newName: "Id");
        }
    }
}
