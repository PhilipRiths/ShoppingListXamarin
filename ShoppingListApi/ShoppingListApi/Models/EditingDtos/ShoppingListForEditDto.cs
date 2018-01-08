using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Models
{
    public class ShoppingListForEditDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public DateTime LastEdited { get; set; }
    }
}
