using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class ShoppingListUser
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}