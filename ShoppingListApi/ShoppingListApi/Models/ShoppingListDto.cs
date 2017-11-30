using ShoppingListApi.Entities;
using System;

namespace ShoppingListApi.Models
{
    public class ShoppingListDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime LastEdited { get; set; }

        public User LastEditedBy { get; set; }

        public User CreatedBy { get; set; }
    }
}