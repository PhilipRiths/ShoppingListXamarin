using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using ShoppingListApi.Services.Interfaces;
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

        public IEnumerable<ShoppingListItem> GetAllShoppingListsAndItems()
        {
            return _context.ShoppingListItem
                .OrderBy(o => o.ShoppingList.Name)
                .Include(sl => sl.ShoppingList)
                .Include(si => si.ShoppingItem)
                .ToList();
        }

        public IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByItemId(int shoppingItemId)
        {
            return _context.ShoppingListItem
                .Where(s => s.ShoppingItemId == shoppingItemId)
                .Include(sl => sl.ShoppingList)
                .ToList();
        }

        public IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByListId(int shoppingListId)
        {
            return _context.ShoppingListItem
                .Where(s => s.ShoppingListId == shoppingListId)
                .Include(si => si.ShoppingItem)
                .ToList();
        }

        public bool ShoppingListExists(int shoppingListId)
        {
            return _context.ShoppingListItem
                .Any(s => s.ShoppingListId == shoppingListId);
        }

        public bool ShoppingItemExists(int shoppingItemId)
        {
            return _context.ShoppingListItem
                .Any(s => s.ShoppingItemId == shoppingItemId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}