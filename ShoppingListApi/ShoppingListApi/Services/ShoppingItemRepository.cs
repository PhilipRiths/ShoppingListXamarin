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

        public void AddShoppingItem(ShoppingItem shoppingItem)
        {
            shoppingItem.Id = Guid.NewGuid();
            _context.ShoppingItems.Add(shoppingItem);
        }

        public IEnumerable<ShoppingItem> GetAllShoppingItems()
        {
            return _context.ShoppingItems
                .OrderBy(s => s.Name)
                .ToList();
        }

        public ShoppingItem GetShoppingItem(Guid id)
        {
            return _context.ShoppingItems
                .FirstOrDefault(si => si.Id == id);
        }

        public void DeleteShoppingItem(ShoppingItem shoppingItem)
        {
            _context.ShoppingItems.Remove(shoppingItem);
        }

        public void DeleteShoppingListItemContainingShoppingItem(Guid shoppingItemId)
        {
            foreach (var listItem in _context.ShoppingListItem)
            {
                if (listItem.ShoppingItemId.Equals(shoppingItemId))
                {
                    _context.ShoppingListItem.Remove(listItem);
                }
            }
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