using Microsoft.AspNetCore.SignalR;
using ShoppingListApi.Entities;
using ShoppingListApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi
{
    public class ShoppingListMessageHub : Hub
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListMessageHub()
        {
            _shoppingListRepository = new ShoppingListRepository();
        }

        public Task AddShoppingList(ShoppingList shoppingList)
        {
            _shoppingListRepository.AddShoppingList(shoppingList);

            return Clients.All.InvokeAsync("ShoppingListAdded", shoppingList);
        }
    }
}
