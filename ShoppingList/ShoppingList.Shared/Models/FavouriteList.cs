using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Shared.Models
{
    public class FavouriteList
    {
        public int Id { get; set; }
        public List<GroceryItem> Items { get; set; }
        public User User { get; set; }
    }
}
