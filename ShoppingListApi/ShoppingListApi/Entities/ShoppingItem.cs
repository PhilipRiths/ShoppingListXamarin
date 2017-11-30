using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApi.Entities
{
    public class ShoppingItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsBought { get; set; }

        public ICollection<ShoppingListItem> ShoppingLists { get; set; } = new List<ShoppingListItem>();
    }
}