using ShoppingListApi.Entities;
using System.Collections.Generic;

namespace ShoppingListApi.Services.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);

        IEnumerable<User> GetUsers();

        User GetUser(int id);

        bool UserExists(int userId);

        void EditUser(User user);

        bool Save();

        void DeleteUser(User userFromRepo);

        void DeleteShoppingListUserContainingUser(int shoppingListId);
    }
}