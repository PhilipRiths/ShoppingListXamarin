using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class ShoppingList
    {
        public ShoppingList()
        {
            Users = new List<ShoppingListUser>();
            ShoppingItems = new List<ShoppingListItem>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<ShoppingListItem> ShoppingItems { get; set; }

        public ICollection<ShoppingListUser> Users { get; set; }

        public DateTime LastEdited { get; set; }

        public User LastEditedBy { get; set; }

        public User CreatedBy { get; set; }
    }
}