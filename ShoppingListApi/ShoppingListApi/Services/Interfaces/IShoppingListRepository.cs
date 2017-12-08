using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public interface IShoppingListRepository
    {
        void AddShoppingList(ShoppingList shoppingList);

        IEnumerable<ShoppingList> GetShoppingLists();

        ShoppingList GetShoppingList(Guid id);

        bool ShoppingListExists(Guid shoppingListId);

        void EditShoppingList(ShoppingList shoppingList);

        bool Save();

        void DeleteShoppingList(ShoppingList shoppingListFromRepo);

        void DeleteShoppingListItemContainingShoppingList(Guid shoppingListId);
    }
}