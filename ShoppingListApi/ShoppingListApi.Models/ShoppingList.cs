using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListApi.Models
{
    public class ShoppingList
    {
        public ShoppingList()
        {
            Users = new List<User>();
            ShoppingItems = new List<ShoppingItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ShoppingItem> ShoppingItems { get; set; }

        public ICollection<User> Users { get; set; }

        public DateTime LastEdited { get; set; }

        public User LastEditedBy { get; set; }

        public User CreatedBy { get; set; }
    }
}
