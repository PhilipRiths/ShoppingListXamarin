using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Migrations
{
    public partial class AddingFavoriteItemsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Mail",
                table: "Users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShoppingLists_Name",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "ShoppingItems");

            migrationBuilder.CreateTable(
                name: "FavoriteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteItems_UserId",
                table: "FavoriteItems",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "ShoppingItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Mail",
                table: "Users",
                column: "Mail");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShoppingLists_Name",
                table: "ShoppingLists",
                column: "Name");
        }
    }
}
