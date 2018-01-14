using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Migrations
{
    public partial class RemovedUniqueConstrainOnShoppingItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShoppingItems_Name",
                table: "ShoppingItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShoppingItems_Name",
                table: "ShoppingItems",
                column: "Name");
        }
    }
}
