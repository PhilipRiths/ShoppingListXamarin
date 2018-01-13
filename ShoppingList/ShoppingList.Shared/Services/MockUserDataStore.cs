using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.Services
{
    public class MockUserDataStore : IDataStore<User>, IUserDataStore
    {
        private readonly List<User> _users;

        public MockUserDataStore()
        {
            _users = new List<User>();
            LoadUsers();
        }

        public async Task<bool> AddAsync(User user)
        {
            _users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _users.Remove(_users.FirstOrDefault(u => u.Id == id));

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteUserByEmailAsync(string email)
        {
            _users.Remove(_users.FirstOrDefault(u => u.Email == email));

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_users);
        }

        public async Task<User> GetAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(s => s.Id == id));
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _users.Remove(_users.FirstOrDefault(u => u.Id == user.Id));
            _users.Add(user);

            return await Task.FromResult(true);
        }

        private void LoadUsers()
        {
            var user1 = new User
            {
                Id = 1,
                FirstName = "Kent",
                LastName = "Anderson",
                Email = "kent.anderson@gmail.com",
                IsNotifyGroceryItemAdded = true,
                IsNotifyGroceryItemUpdated = false,
                IsNotifyGroceryItemDeleted = true
            };
            var user2 = new User
            {
                Id = 2,
                FirstName = "Rachel",
                LastName = "Addison",
                Email = "rachel.addison@gmail.com",
                IsNotifyGroceryItemAdded = false,
                IsNotifyGroceryItemUpdated = false,
                IsNotifyGroceryItemDeleted = true
            };
            var user3 = new User
            {
                Id = 3,
                FirstName = "Elia",
                LastName = "Gabor",
                Email = "saga.gabor@gmail.com",
                IsNotifyGroceryItemAdded = false,
                IsNotifyGroceryItemUpdated = false,
                IsNotifyGroceryItemDeleted = false
            };
            var user4 = new User
            {
                Id = 4,
                FirstName = "Bean",
                LastName = "Dabney",
                Email = "bean.dabney@gmail.com",
                IsNotifyGroceryItemAdded = true,
                IsNotifyGroceryItemUpdated = true,
                IsNotifyGroceryItemDeleted = true
            };

            _users.Add(user1);
            _users.Add(user2);
            _users.Add(user3);
            _users.Add(user4);
        }
    }
}