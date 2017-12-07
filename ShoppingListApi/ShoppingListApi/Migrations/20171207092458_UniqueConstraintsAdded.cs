using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Migrations
{
    public partial class UniqueConstraintsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_GoogleId",
                table: "Users",
                column: "GoogleId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Mail",
                table: "Users",
                column: "Mail");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShoppingLists_Name",
                table: "ShoppingLists",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShoppingItems_Name",
                table: "ShoppingItems",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_GoogleId",
                table: "Users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Mail",
                table: "Users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShoppingLists_Name",
                table: "ShoppingLists");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShoppingItems_Name",
                table: "ShoppingItems");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
