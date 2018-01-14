using ShoppingList.Shared.Wrappers;

namespace ShoppingList.Shared.Services
{
    using Newtonsoft.Json;
    using ShoppingList.Shared.ApiService;
    using ShoppingList.Shared.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MockGroceryListDataStore : IDataStore<GroceryList>, IGroceryListDataStore
    {
        private readonly List<GroceryList> _groceryLists;
        private readonly MockUserDataStore _users;

        public MockGroceryListDataStore()
        {
            _groceryLists = new List<GroceryList>();
            _users = new MockUserDataStore();
            LoadShoppingLists();
        }

        public async Task<bool> AddAsync(GroceryList list)
        {
            _groceryLists.Add(list);

            var shoppingListJson = JsonConvert.SerializeObject(
                new
                {
                    shoppingList = list
                });

            var restClient = new RestClient
            {
                // Is that an IP-Address?
                _endPoint = "https://192.168.1.119:3333/api/ShoppingLists",
                _httpMethod = httpVerb.POST,
                PostJSON = shoppingListJson
            };

            await restClient.MakeRequest();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var removeList = _groceryLists.FirstOrDefault(arg => arg.Id == id);
            _groceryLists.Remove(removeList);

            var restClient = new RestClient
            {
                _endPoint = "https://192.168.1.119:3333/api/ShoppingLists/" + id,
                _httpMethod = httpVerb.DELETE
            };

            await restClient.MakeRequest();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSharedListUser(int listId, UserWrapper selectedUser)
        {
            var groceryListFromDb = _groceryLists.Find(g => g.Id == listId);
            var user = groceryListFromDb.Users.FirstOrDefault(u => u.Email == selectedUser.Email);

            groceryListFromDb.Users.Remove(user);

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
                    _endPoint = "https://192.168.1.119:3333/api/ShoppingLists"
                };
                var groceryLists = await restClient.MakeRequest();
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
                var grocyList1 = new GroceryList
                {
                    Id = 1,
                    Name = "Babas lista",
                    Owner = await _users.GetAsync(1),
                    Items = new List<GroceryItem>
                {
                    new GroceryItem { Id = 1, Name = "Banan", InBasket = false },
                    new GroceryItem { Id = 2, Name = "Äpple", InBasket = false },
                    new GroceryItem { Id = 3, Name = "Yoghurt", InBasket = false },
                    new GroceryItem { Id = 4, Name = "Kanel", InBasket = false },
                },
                    Users = new List<User> { await _users.GetAsync(1), await _users.GetAsync(2) }
                };

                var grocyList2 = new GroceryList
                {
                    Id = 2,
                    Name = "Babas lista2",
                    Owner = await _users.GetAsync(2),
                    Items = new List<GroceryItem>
                {
                    new GroceryItem { Id = 5, Name = "Avokado", InBasket = false },
                    new GroceryItem { Id = 6, Name = "Spetskål", InBasket = false },
                    new GroceryItem { Id = 7, Name = "Gurka", InBasket = false },
                    new GroceryItem { Id = 8, Name = "Keso", InBasket = false },
                },
                    Users = new List<User>(await _users.GetAllAsync())
                };

                _groceryLists.Add(grocyList1);
                _groceryLists.Add(grocyList2);
            }
        }
    }
}