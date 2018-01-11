using System;
using ShoppingListApi.Entities;
using System.Collections.Generic;

namespace ShoppingListApi.Services.Interfaces
{
    public interface IShoppingListUserRepository
    {
        void AddShoppingListUser(ShoppingListUser shoppingListUser);
        IEnumerable<ShoppingListUser> GetShoppingListUsers();
        ShoppingListUser GetShoppingListUser(string googleId);
        void EditShoppingListUser(ShoppingListUser shoppingListUser);
        void DeleteShoppingListUser(ShoppingListUser shoppingListUser);
        bool ShoppingListUserExists(string googleId);
        bool Save();
    }
}