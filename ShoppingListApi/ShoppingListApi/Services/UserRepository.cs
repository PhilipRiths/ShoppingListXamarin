using ShoppingListApi.Entities;
using ShoppingListApi.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteShoppingListUserContainingUser(int shoppingListId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User userFromRepo)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int userId)
        {
            throw new NotImplementedException();
        }
    }
}