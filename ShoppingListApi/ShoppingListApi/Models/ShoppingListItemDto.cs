using ShoppingListApi.Entities;
using System;

namespace ShoppingListApi.Models
{
    public class ShoppingListItemDto
    {
        public Guid Id { get; set; }

        public Guid ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public Guid ShoppingItemId { get; set; }
        public ShoppingItem ShoppingItem { get; set; }
    }
}