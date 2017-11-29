using ShoppingListApi.Entities;
using System.Collections.Generic;

namespace ShoppingListApi.Services
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        public ShoppingListRepository()
        {
        }

        public void AddShoppingList(ShoppingList shoppingList)
        {
        }

        public IEnumerable<ShoppingList> GetShoppingLists()
        {
            throw new System.NotImplementedException();
        }
    }
}