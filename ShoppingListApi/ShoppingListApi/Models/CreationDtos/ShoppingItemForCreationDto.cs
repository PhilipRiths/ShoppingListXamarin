using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Models
{
    public class ShoppingItemForCreationDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsBought { get; set; }

        public ICollection<ShoppingListItem> ShoppingLists { get; set; } = new List<ShoppingListItem>();
    }
}