﻿using Microsoft.EntityFrameworkCore;
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

            if (shoppingList.ShoppingItems.Any())
            {
                foreach (var item in shoppingList.ShoppingItems)
                {
                    item.Id = Guid.NewGuid();
                }
            }
        }

        public IEnumerable<ShoppingList> GetShoppingLists()
        {
            return _context.ShoppingLists
                .OrderBy(s => s.Name)
                .Include(u => u.Users)
                .Include(c => c.CreatedBy)
                .Include(l => l.LastEditedBy)
                .ToList();
        }

        public ShoppingList GetShoppingList(Guid id)
        {
            return _context.ShoppingLists
                .Include(si => si.ShoppingItems)
                .Include(u => u.Users)
                .Include(c => c.CreatedBy)
                .Include(l => l.LastEditedBy)
                .FirstOrDefault(s => s.Id == id);
        }

        public void DeleteShoppingList(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Remove(shoppingList);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool ShoppingListExists(Guid shoppingListId)
        {
            return _context.ShoppingLists.Any(s => s.Id.Equals(shoppingListId));
        }
    }
}