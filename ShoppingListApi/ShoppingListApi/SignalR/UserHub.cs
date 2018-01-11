using Microsoft.AspNetCore.SignalR;
using ShoppingListApi.Services.Interfaces;
using ShoppingListApi.SignalR.Interfaces;

namespace ShoppingListApi.SignalR
{
    public class UserHub : Hub, IUserHub
    {
        public UserHub(IUserRepository UserRepository)
        {

        }
    }
}