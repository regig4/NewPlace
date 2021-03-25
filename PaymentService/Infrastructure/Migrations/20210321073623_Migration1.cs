using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PointsValue_Amount",
                schema: "payment",
                table: "Payments",
                newName: "points_amount");

            migrationBuilder.RenameColumn(
                name: "MoneyValue_Currency",
                schema: "payment",
                table: "Payments",
                newName: "money_currency");

            migrationBuilder.RenameColumn(
                name: "MoneyValue_Amount",
                schema: "payment",
                table: "Payments",
                newName: "money_amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "points_amount",
                schema: "payment",
                table: "Payments",
                newName: "PointsValue_Amount");

            migrationBuilder.RenameColumn(
                name: "money_currency",
                schema: "payment",
                table: "Payments",
                newName: "MoneyValue_Currency");

            migrationBuilder.RenameColumn(
                name: "money_amount",
                schema: "payment",
                table: "Payments",
                newName: "MoneyValue_Amount");
        }
    }
}
