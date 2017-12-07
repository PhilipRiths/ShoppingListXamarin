using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingListApi.Models
{
    public class ShoppingListUserDto
    {
        public Guid Id { get; set; }

        public Guid ShoppingListId { get; set; }
        public ShoppingListDto ShoppingList { get; set; }

        public Guid UserId { get; set; }
        public UserDto User { get; set; }
    }
}
