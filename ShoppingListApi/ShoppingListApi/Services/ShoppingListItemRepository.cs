﻿using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<ShoppingListItem> GetAllShoppingListsAndItems()
        {
            return _context.ShoppingListItem
                .OrderBy(o => o.ShoppingList.Name)
                .Include(sl => sl.ShoppingList)
                .Include(si => si.ShoppingItem)
                .ToList();
        }

        public IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByItemId(Guid shoppingItemId)
        {
            return _context.ShoppingListItem
                .Where(s => s.ShoppingItemId == shoppingItemId)
                .Include(sl => sl.ShoppingList)
                .Include(si => si.ShoppingItem)
                .ToList();
        }

        public IEnumerable<ShoppingListItem> GetShoppingListsAndItemsByListId(Guid shoppingListId)
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

        public bool ShoppingItemExists(Guid shoppingItemId)
        {
            return _context.ShoppingItems.Any(i => i.Id.Equals(shoppingItemId));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}