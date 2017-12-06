using System;
using ShoppingListApi.Entities;

namespace ShoppingListApi.Services
{
    public interface IUserRepository
    {
        User GetUser(string googleId);
    }
}