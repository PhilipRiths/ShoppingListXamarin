namespace ShoppingList.Shared.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using ShoppingList.Shared.Models;
    using ShoppingList.Shared.ApiService;
    using Newtonsoft.Json;

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

        public async Task<bool> DeleteAsync(int id)
        {
            var removeList = _groceryLists.FirstOrDefault(arg => arg.Id == id);
            _groceryLists.Remove(removeList);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<GroceryList>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_groceryLists);
        }

        public async Task<GroceryList> GetAsync(int id)
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

        private async void LoadShoppingLists()
        {
            try
            {
                var restClient = new RestClient
                {
                    _endPoint = "http://192.168.1.225:3000/api/ShoppingLists"
                };
                var groceryLists = await restClient.MakeRequest();
                if (groceryLists.Equals("ERROR"))
                {
                    return;
                }
                var Lists = JsonConvert.DeserializeObject<dynamic>(groceryLists);

                foreach (var list in Lists)
                {
                    _groceryLists.Add(
                            new GroceryList
                            {
                                Id = list.Id,
                                Name = list.Name
                            }
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}