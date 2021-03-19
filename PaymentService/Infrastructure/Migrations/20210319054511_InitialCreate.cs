using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payment");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payee_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_type = table.Column<int>(type: "int", nullable: false),
                    payment_status = table.Column<int>(type: "int", nullable: false),
                    PointsValue_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MoneyValue_Amount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    MoneyValue_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    money_value_temp_id = table.Column<int>(type: "int", nullable: true),
                    points_value_temp_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payments_User_payee_id",
                        column: x => x.payee_id,
                        principalSchema: "payment",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_User_payer_id",
                        column: x => x.payer_id,
                        principalSchema: "payment",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_payee_id",
                schema: "payment",
                table: "Payments",
                column: "payee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_payer_id",
                schema: "payment",
                table: "Payments",
                column: "payer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "User",
                schema: "payment");
        }
    }
}
