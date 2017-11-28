using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListApi.Models
{
    public class User
    {
        public User()
        {
            ShoppingLists = new List<ShoppingList>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
