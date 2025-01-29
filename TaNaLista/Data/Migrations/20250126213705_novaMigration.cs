using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaNaLista.Data.Migrations
{
    /// <inheritdoc />
    public partial class novaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProducts_Products_ProductId",
                table: "ShoppingListProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProducts_ShoppingLists_ShoppingListId",
                table: "ShoppingListProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_Users_UserId",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListProducts",
                table: "ShoppingListProducts");

            migrationBuilder.RenameTable(
                name: "ShoppingListProducts",
                newName: "ShoppingListProduct");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListProducts_ProductId",
                table: "ShoppingListProduct",
                newName: "IX_ShoppingListProduct_ProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "last name",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ShoppingLists",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListProduct",
                table: "ShoppingListProduct",
                columns: new[] { "ShoppingListId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId1",
                table: "ShoppingLists",
                column: "UserId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_Users_UserId1",
                table: "ShoppingLists",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProduct_Products_ProductId",
                table: "ShoppingListProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListProduct_ShoppingLists_ShoppingListId",
                table: "ShoppingListProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_Users_UserId1",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_UserId1",
                table: "ShoppingLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListProduct",
                table: "ShoppingListProduct");

            migrationBuilder.DropColumn(
                name: "last name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ShoppingLists");

            migrationBuilder.RenameTable(
                name: "ShoppingListProduct",
                newName: "ShoppingListProducts");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListProduct_ProductId",
                table: "ShoppingListProducts",
                newName: "IX_ShoppingListProducts_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListProducts",
                table: "ShoppingListProducts",
                columns: new[] { "ShoppingListId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_Users_UserId",
                table: "ShoppingLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
