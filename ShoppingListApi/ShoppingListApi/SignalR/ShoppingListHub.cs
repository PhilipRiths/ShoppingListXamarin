using Microsoft.AspNetCore.SignalR;
using ShoppingListApi.Entities;
using ShoppingListApi.Services;
using System;
using System.Threading.Tasks;

namespace ShoppingListApi
{
    public class ShoppingListHub : Hub, IShoppingListHub
    {
        private IShoppingListRepository _shoppingListRepository;

        public ShoppingListHub(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public Task AddShoppingList(ShoppingList shoppingList)
        {
            return Clients.All.InvokeAsync("ShoppingListAdded", shoppingList);
        }

        public Task UpdateProduct(ShoppingList shoppingList)
        {
            return Clients.All.InvokeAsync("ShoppingListUpdated", shoppingList);
        }

        public Task RemoveShoppingList(Guid shoppingListId)
        {
            return Clients.All.InvokeAsync("ShoppingListRemoved", shoppingListId);
        }
    }
}