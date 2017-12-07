using System;

namespace ShoppingListApi.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Mail { get; set; }

        public string GoogleId { get; set; }
    }
}