using ShoppingListApi.Entities;
using System;
using System.Threading.Tasks;

namespace ShoppingListApi
{
    public interface IShoppingListHub
    {
        Task AddShoppingList(ShoppingList shoppingList);

        Task RemoveShoppingList(Guid shoppingListId);

        Task UpdateProduct(ShoppingList shoppingList);
    }
}