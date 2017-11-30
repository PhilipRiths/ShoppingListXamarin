using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Shared.Models;
using ShoppingList = ShoppingList.Shared.Models.ShoppingLists;

namespace ShoppingList.Shared.Services
{
    public class MockDataStore : IDataStore<ShoppingLists>
    {
        private List<ShoppingLists> _shoppingLists;
        public MockDataStore()
        {
            _shoppingLists = new List<ShoppingLists>();
            var mockLists = new List<ShoppingLists>()
            {
                new ShoppingLists()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kalles lista",
                    Items = new ObservableCollection<Items>()
                    {
                        new Items()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Sylt",
                            InBasket = false

                        },
                        new Items()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Pannkakor",
                            InBasket = false

                        },
                        new Items()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Yoghurt",
                            InBasket = false

                        },
                        new Items()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Kanel",
                            InBasket = false

                        },
                    }
                },
                new ShoppingLists()
                {
                Id = Guid.NewGuid().ToString(),
                Name = "Babas lista",
                Items = new ObservableCollection<Items>()
                {
                    new Items()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Banan",
                        InBasket = false

                    },
                    new Items()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Äpple",
                        InBasket = false

                    },
                    new Items()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Yoghurt",
                        InBasket = false

                    },
                    new Items()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Kanel",
                        InBasket = false

                    },
                }
                
            }
            
            };
            foreach (var item in mockLists)
            {
                
                _shoppingLists.Add(item);
            }
        }

    
    public async Task<bool> AddAsync(ShoppingLists list)
    {
        _shoppingLists.Add(list);

        return await Task.FromResult(true);
    }

        public async Task<bool> UpdateAsync(ShoppingLists list)
    {
        var _updateList = _shoppingLists.Where((ShoppingLists arg) => arg.Id == list.Id).FirstOrDefault();
        _shoppingLists.Remove(_updateList);
        _shoppingLists.Add(list);

        return await Task.FromResult(true);
        }

    public async Task<bool> DeleteAsync(string id)
    {
        var _removeList = _shoppingLists.Where((ShoppingLists arg) => arg.Id == id).FirstOrDefault();
        _shoppingLists.Remove(_removeList);

        return await Task.FromResult(true);
        }

    public async Task<ShoppingLists> GetAsync(string id)
    {
        return await Task.FromResult(_shoppingLists.FirstOrDefault(s => s.Id == id));
        }

    public async  Task<IEnumerable<ShoppingLists>> GetAllAsync(bool forceRefresh = false)
    {
        return await Task.FromResult(_shoppingLists);
        }
}
}
