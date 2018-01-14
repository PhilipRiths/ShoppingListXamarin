using ShoppingListApi.Entities;
using System.Collections.Generic;

namespace ShoppingListApi.Models
{
    public class ShoppingListUserForCreationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string GoogleId { get; set; }

        public ICollection<ShoppingListUser> ShoppingLists { get; set; } = new List<ShoppingListUser>();
    }
}