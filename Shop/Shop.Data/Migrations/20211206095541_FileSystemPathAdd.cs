using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.Migrations
{
    public partial class FileSystemPathAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExistingFilePaths_ProductId",
                table: "ExistingFilePaths",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExistingFilePaths_Product_ProductId",
                table: "ExistingFilePaths",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExistingFilePaths_Product_ProductId",
                table: "ExistingFilePaths");

            migrationBuilder.DropIndex(
                name: "IX_ExistingFilePaths_ProductId",
                table: "ExistingFilePaths");
        }
    }
}
