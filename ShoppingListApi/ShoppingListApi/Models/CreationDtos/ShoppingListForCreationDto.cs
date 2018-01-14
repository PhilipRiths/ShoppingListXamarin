using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Models
{
    public class ShoppingListForCreationDto
    {
        public string Name { get; set; }

        public ICollection<ShoppingListItem> ShoppingItems { get; set; } = new List<ShoppingListItem>();

        public ICollection<ShoppingListUser> Users { get; set; } = new List<ShoppingListUser>();

        public DateTime LastEdited { get; set; }

        public User LastEditedBy { get; set; }

        public User CreatedBy { get; set; }
    }
}