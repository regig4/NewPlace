using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: false),
                    information = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    apartment_type = table.Column<int>(nullable: false),
                    pricing_type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(nullable: false),
                    postal_code = table.Column<string>(nullable: false),
                    city = table.Column<string>(nullable: false),
                    latitude = table.Column<double>(nullable: false),
                    longitude = table.Column<double>(nullable: false),
                    radius = table.Column<double>(nullable: false),
                    accuracy = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    login = table.Column<string>(nullable: false),
                    password_hash = table.Column<string>(maxLength: 512, nullable: false),
                    email = table.Column<string>(nullable: false),
                    agency_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Agency_agency_id",
                        column: x => x.agency_id,
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estate",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    area = table.Column<double>(nullable: false),
                    location_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estate", x => x.id);
                    table.ForeignKey(
                        name: "FK_Estate_Location_location_id",
                        column: x => x.location_id,
                        principalTable: "Location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    category_id = table.Column<int>(nullable: false),
                    create_date = table.Column<DateTime>(nullable: false),
                    validity_time = table.Column<TimeSpan>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    estate_id = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    provision = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.id);
                    table.ForeignKey(
                        name: "FK_Advertisement_Category_category_id",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisement_Estate_estate_id",
                        column: x => x.estate_id,
                        principalTable: "Estate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisement_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utility",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    cost = table.Column<decimal>(nullable: true),
                    estate_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utility", x => x.id);
                    table.ForeignKey(
                        name: "FK_Utility_Estate_estate_id",
                        column: x => x.estate_id,
                        principalTable: "Estate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_category_id",
                table: "Advertisement",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_estate_id",
                table: "Advertisement",
                column: "estate_id");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_user_id",
                table: "Advertisement",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estate_location_id",
                table: "Estate",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_agency_id",
                table: "User",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "IX_Utility_estate_id",
                table: "Utility",
                column: "estate_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "Utility");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Estate");

            migrationBuilder.DropTable(
                name: "Agency");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
