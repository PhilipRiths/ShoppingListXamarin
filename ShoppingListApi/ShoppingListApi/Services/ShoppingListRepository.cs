using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using System;
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
            shoppingList.Id = Guid.NewGuid();
            _context.ShoppingLists.Add(shoppingList);
        }

        public IEnumerable<ShoppingListItem> GetShoppingListItem(Guid shoppingListId)
        {
            return _context.ShoppingListItem
                .Where(s => s.ShoppingListId == shoppingListId)
                .Include(si => si.ShoppingItem)
                .Include(sl => sl.ShoppingList)
                .ToList();
        }

        public IEnumerable<ShoppingList> GetShoppingLists()
        {
            return _context.ShoppingLists
                .OrderBy(s => s.Name)
                .Include(su => su.Users)
                .ToList();
        }

        public ShoppingList GetShoppingList(Guid id)
        {
            return _context.ShoppingLists
                .Include(si => si.ShoppingItems)
                .FirstOrDefault(s => s.Id == id);
        }
    }
}