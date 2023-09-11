using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayerApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SetDeleteBehaviorOfFK_Product_ProductFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductFeature",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductFeature",
                table: "Products",
                column: "ProductFeatureId",
                principalTable: "ProductFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductFeature",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductFeature",
                table: "Products",
                column: "ProductFeatureId",
                principalTable: "ProductFeatures",
                principalColumn: "Id");
        }
    }
}
