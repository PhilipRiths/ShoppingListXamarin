using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using ShoppingListApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListApi.Services
{
    public class ShoppingListUserRepository : IShoppingListUserRepository
    {
        private ShoppingListContext _context;

        public ShoppingListUserRepository(ShoppingListContext context)
        {
            _context = context; 
        }
         
        public void AddShoppingListUser(ShoppingListUser shoppingListUser)
        {
            shoppingListUser.Id = Guid.NewGuid();
            _context.ShoppingListUser.Add(shoppingListUser);
        }

        public IEnumerable<ShoppingListUser> GetShoppingListUsers()
        {
            return _context.ShoppingListUser
                .OrderBy(slu => slu.User.FirstName)
                .Include(slu => slu.ShoppingList)
                .Include(slu => slu.User); 
        }

        public ShoppingListUser GetShoppingListUser(string googleId)
        {
            return _context.ShoppingListUser
                .Where(slu => slu.User.GoogleId.Equals(googleId))
                .Include(slu => slu.User.ShoppingLists)
                .SingleOrDefault();
        }
        
        public void EditShoppingListUser(ShoppingListUser shoppingListUser)
        {
            var shoppingListUserFromRepo = _context.ShoppingListUser.FirstOrDefault(slu => slu.Id == shoppingListUser.Id);

            shoppingListUserFromRepo.User.FirstName = shoppingListUser.User.FirstName;
            shoppingListUserFromRepo.User.LastName = shoppingListUser.User.LastName;
        }

        public void DeleteShoppingListUser(ShoppingListUser shoppingListUser)
        {
            _context.ShoppingListUser.Remove(shoppingListUser);
        }

        public bool ShoppingListUserExists(string googleId)
        {
            return _context.ShoppingListUser
                .Any(slu => slu.User.GoogleId.Equals(googleId));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}