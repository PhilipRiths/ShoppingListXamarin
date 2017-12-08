using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListApi.Services
{
    public class ShoppingListItemRepository : IShoppingListItemRepository
    {
        private ShoppingListContext _context;

        public ShoppingListItemRepository(ShoppingListContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingListItem> GetAllShoppingListItems()
        {
            return _context.ShoppingListItem
                .OrderBy(o => o.ShoppingList.Name)
                .Include(sl => sl.ShoppingList)
                .Include(si => si.ShoppingItem)
                .ToList();
        }

        public IEnumerable<ShoppingListItem> GetShoppingListItem(Guid shoppingListId)
        {
            return _context.ShoppingListItem
                .Where(s => s.ShoppingListId == shoppingListId)
                .Include(sl => sl.ShoppingList)
                .Include(si => si.ShoppingItem)
                .ToList();
        }

        public bool ShoppingListExists(Guid shoppingListId)
        {
            return _context.ShoppingLists.Any(s => s.Id.Equals(shoppingListId));
        }
    }
}