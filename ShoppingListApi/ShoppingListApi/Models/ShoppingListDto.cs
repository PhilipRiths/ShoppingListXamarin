using System;

namespace ShoppingListApi.Models
{
    public class ShoppingListDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime LastEdited { get; set; }

        public UserDto LastEditedBy { get; set; }

        public UserDto CreatedBy { get; set; }
    }
}