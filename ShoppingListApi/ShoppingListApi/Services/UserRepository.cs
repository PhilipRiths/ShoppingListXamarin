using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListApi.Services
{
    public class UserRepository : IUserRepository
    {
        private ShoppingListContext _context;

        public UserRepository(ShoppingListContext context)
        {
            _context = context; 
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users
                .OrderBy(u => u.FirstName)
                .Include(u => u.ShoppingLists); 
        }

        public User GetUser(string googleId)
        {
            return new User();

            //return _context.Users
            //    .Where(u => u.GoogleId.Equals(googleId))
            //    .Include(u => u.ShoppingLists)
            //    .SingleOrDefault();
        }
    }
}