using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindDal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    company_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    contact_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    contact_title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    address = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    city = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    region = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    postal_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    country = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    phone = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    fax = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<short>(type: "smallint", nullable: false),
                    product_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    supplier_id = table.Column<short>(type: "smallint", nullable: true),
                    category_id = table.Column<short>(type: "smallint", nullable: true),
                    quantity_per_unit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    unit_price = table.Column<float>(type: "real", nullable: true),
                    units_in_stock = table.Column<short>(type: "smallint", nullable: true),
                    units_on_order = table.Column<short>(type: "smallint", nullable: true),
                    reorder_level = table.Column<short>(type: "smallint", nullable: true),
                    discontinued = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<short>(type: "smallint", nullable: false),
                    customer_id = table.Column<string>(type: "string", nullable: true),
                    employee_id = table.Column<short>(type: "smallint", nullable: true),
                    order_date = table.Column<DateOnly>(type: "date", nullable: true),
                    required_date = table.Column<DateOnly>(type: "date", nullable: true),
                    shipped_date = table.Column<DateOnly>(type: "date", nullable: true),
                    ship_via = table.Column<short>(type: "smallint", nullable: true),
                    freight = table.Column<float>(type: "real", nullable: true),
                    ship_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    ship_address = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    ship_city = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    ship_region = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    ship_postal_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ship_country = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "fk_orders_customers",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    order_id = table.Column<short>(type: "smallint", nullable: false),
                    product_id = table.Column<short>(type: "smallint", nullable: false),
                    unit_price = table.Column<float>(type: "real", nullable: false),
                    quantity = table.Column<short>(type: "smallint", nullable: false),
                    discount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_details", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_order_details_orders",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "fk_order_details_products",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_details_product_id",
                table: "order_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
