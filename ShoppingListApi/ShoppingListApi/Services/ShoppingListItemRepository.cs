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

        public ShoppingListItem GetShoppingListItemById(Guid id)
        {
            return _context.ShoppingListItem
                .Include(si => si.ShoppingItem)
                .FirstOrDefault(si => si.ShoppingItemId == id);
        }

        public IEnumerable<ShoppingListItem> GetShoppingListItems(Guid shoppingListId)
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

        public void EditShoppingItem(ShoppingItem shoppingItem)
        {
            var shoppingItemFromRepo = _context.ShoppingItems.FirstOrDefault(i => i.Id == shoppingItem.Id);

            shoppingItemFromRepo.Quantity = shoppingItem.Quantity;
            shoppingItemFromRepo.Description = shoppingItem.Description;
            shoppingItemFromRepo.IsFavorite = shoppingItem.IsFavorite;
            shoppingItemFromRepo.IsBought = shoppingItem.IsBought;
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}