using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayerApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFK_Product_ProductFeatureToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductFeatureId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductFeatureId",
                table: "Products",
                column: "ProductFeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductFeatureId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductFeatureId",
                table: "Products",
                column: "ProductFeatureId",
                unique: true);
        }
    }
}
