namespace ShoppingList.Shared.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using ShoppingList.Shared.Models;

    public class MockGroceryListDataStore : IDataStore<GroceryList>
    {
        private readonly List<GroceryList> _groceryLists;

        public MockGroceryListDataStore()
        {
            _groceryLists = new List<GroceryList>();
            LoadShoppingLists();
        }

        public async Task<bool> AddAsync(GroceryList list)
        {
            _groceryLists.Add(list);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var removeList = _groceryLists.FirstOrDefault(arg => arg.Id == id);
            _groceryLists.Remove(removeList);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<GroceryList>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_groceryLists);
        }

        public async Task<GroceryList> GetAsync(string id)
        {
            return await Task.FromResult(_groceryLists.FirstOrDefault(s => s.Id == id));
        }

        public async Task<bool> UpdateAsync(GroceryList list)
        {
            var updateList = _groceryLists.FirstOrDefault(arg => arg.Id == list.Id);
            _groceryLists.Remove(updateList);
            _groceryLists.Add(list);

            return await Task.FromResult(true);
        }

        private void LoadShoppingLists()
        {
            var grocyList1 = new GroceryList
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Babas lista",
                Items = new ObservableCollection<GroceryItem>
                {
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Banan", InBasket = false },
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Äpple", InBasket = false },
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Yoghurt", InBasket = false },
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Kanel", InBasket = false },
                }
            };

            var grocyList2 = new GroceryList
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Babas lista2",
                Items = new ObservableCollection<GroceryItem>
                {
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Avokado", InBasket = false },
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Spetskål", InBasket = false },
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Gurka", InBasket = false },
                    new GroceryItem { Id = Guid.NewGuid().ToString(), Name = "Keso", InBasket = false },
                }
            };

            _groceryLists.Add(grocyList1);
            _groceryLists.Add(grocyList2);
        }
    }
}