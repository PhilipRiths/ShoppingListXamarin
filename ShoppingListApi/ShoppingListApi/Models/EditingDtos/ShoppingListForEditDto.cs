using System;

namespace ShoppingListApi.Models
{
    public class ShoppingListForEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime LastEdited { get; set; }
    }
}