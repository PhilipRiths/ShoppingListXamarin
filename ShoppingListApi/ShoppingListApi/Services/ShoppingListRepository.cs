using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListApi.Services
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private ShoppingListContext _context;

        public ShoppingListRepository(ShoppingListContext context)
        {
            _context = context;
        }

        public void AddShoppingList(ShoppingList shoppingList)
        {
        }

        public IEnumerable<ShoppingList> GetShoppingLists()
        {
            return _context.ShoppingLists
                .OrderBy(i => i.Name)
                .ToList();
        }
    }
}