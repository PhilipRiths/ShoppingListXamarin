using ShoppingListApi.Entities;
using System;

namespace ShoppingListApi.Services
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }

        public User GetUser(Guid Id)
        {
            return new User();
        }
    }
}