using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaNaLista.Data.migration
{
    /// <inheritdoc />
    public partial class ModificandoIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProduct_Products_ProductId",
                table: "ShoppingListProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProduct_ShoppingLists_ShoppingListId",
                table: "ShoppingListProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListProduct",
                table: "ShoppingListProduct");

            migrationBuilder.RenameTable(
                name: "ShoppingListProduct",
                newName: "ShoppingListProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListProduct_ProductId",
                table: "ShoppingListProducts",
                newName: "IX_ShoppingListProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListProducts",
                table: "ShoppingListProducts",
                columns: new[] { "ShoppingListId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListProducts_Products_ProductId",
                table: "ShoppingListProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListProducts_ShoppingLists_ShoppingListId",
                table: "ShoppingListProducts",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProducts_Products_ProductId",
                table: "ShoppingListProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProducts_ShoppingLists_ShoppingListId",
                table: "ShoppingListProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListProducts",
                table: "ShoppingListProducts");

            migrationBuilder.RenameTable(
                name: "ShoppingListProducts",
                newName: "ShoppingListProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListProducts_ProductId",
                table: "ShoppingListProduct",
                newName: "IX_ShoppingListProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListProduct",
                table: "ShoppingListProduct",
                columns: new[] { "ShoppingListId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListProduct_Products_ProductId",
                table: "ShoppingListProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListProduct_ShoppingLists_ShoppingListId",
                table: "ShoppingListProduct",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
