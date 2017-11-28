using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListApi.Models
{
    public class ShoppingItem
    {
        public ShoppingItem()
        {
            ShoppingLists = new List<ShoppingList>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsBought { get; set; }

        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
