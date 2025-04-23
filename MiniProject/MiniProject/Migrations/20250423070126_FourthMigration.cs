using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_product_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 1, "Electronics" });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "ProductId", "CategoryId", "Description", "Image", "Price", "Title", "stock" },
                values: new object[] { 1, 1, "\r\nIntroducing the ZEB-PIXAPLAY 25, a smart LED projector that features 5500 lumens and supports screen sizes up to 508cm, as it ensures vibrant visuals in any setting. This projector offers seamless connectivity through BT v5.1, HDMI (ARC), USB, and AUX OUT, complemented by a remote control for easy operation. Enjoy FHD 1080p support and a 30,000-hour LED lamp lifespan, with built-in speakers for immersive audio. Equipped with a quad-core processor, dual-band connectivity, Miracast, iOS screen mirroring, and app support, it delivers a modern viewing experience. Advanced sensor tech including auto focus, auto keystone, auto obstacle detection, and auto screenfit enhance usability, making it ideal for diverse multimedia needs.", "https://rukminim2.flixcart.com/image/612/612/xif0q/projector/s/x/v/zeb-pixaplay-25-17-zeb-pixaplay-25-full-hd-zebronics-original-imah5f67nyepnnwv.jpeg?q=70", 10999m, "ZEBRONICS Zeb - Pixaplay 25 (5500 lm) Portable 1080p, 200-inch Screen Size, Quad Core, Auto Focus, Keystone, Obstacle Detection, Screenfit, Bluetooth, WiFi, HDMI-ARC, APP Support, Miracast Smart Projector  (Metallic Grey)", 10 });

            migrationBuilder.CreateIndex(
                name: "IX_product_CategoryId",
                table: "product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
