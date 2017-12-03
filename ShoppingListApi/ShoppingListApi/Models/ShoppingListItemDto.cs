using System;

namespace ShoppingListApi.Models
{
    public class ShoppingListItemDto
    {
        public Guid Id { get; set; }

        public Guid ShoppingListId { get; set; }
        public ShoppingListDto ShoppingList { get; set; }

        public Guid ShoppingItemId { get; set; }
        public ShoppingItemDto ShoppingItem { get; set; }
    }
}