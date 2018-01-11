using System;

namespace ShoppingListApi.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }
    }
}