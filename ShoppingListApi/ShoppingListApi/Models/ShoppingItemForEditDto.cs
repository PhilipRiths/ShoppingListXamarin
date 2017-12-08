﻿using System;

namespace ShoppingListApi.Models
{
    public class ShoppingItemForEditDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsBought { get; set; }
    }
}