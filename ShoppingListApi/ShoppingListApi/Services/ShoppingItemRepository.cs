using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListApi.Services
{
    public class ShoppingItemRepository : IShoppingItemRepository
    {
        private ShoppingListContext _context;

        public ShoppingItemRepository(ShoppingListContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingListItem> GetShoppingListItem(Guid shoppingListId)
        {
            return _context.ShoppingListItem
                .Where(s => s.ShoppingListId == shoppingListId)
                .Include(si => si.ShoppingItem)
                .Include(sl => sl.ShoppingList)
                .ToList();
        }
    }
}