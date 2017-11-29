using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.Services
{
   public class ItemMockDataStore : IDataStore<Items>
    {
        private List<Items> _items;
        public Task<bool> AddAsync(Items item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Items item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Items> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Items>> GetAllAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
