using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class ShoppingList
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<ShoppingListItem> ShoppingItems { get; set; } = new List<ShoppingListItem>();

        public ICollection<ShoppingListUser> Users { get; set; } = new List<ShoppingListUser>();

        public DateTime LastEdited { get; set; }

        public User LastEditedBy { get; set; }

        public User CreatedBy { get; set; }
    }
}