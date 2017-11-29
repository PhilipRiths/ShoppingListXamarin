using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class ShoppingListItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public Guid ShoppingItemId { get; set; }
        public ShoppingItem ShoppingItem { get; set; }
    }
}