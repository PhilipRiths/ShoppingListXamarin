using ShoppingListApi.Data;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Entities
{
    public static class ShoppingListContextExtensions
    {
        public static void EnsureSeedDataForContext(this ShoppingListContext context)
        {
            context.ShoppingLists.RemoveRange(context.ShoppingLists);
            context.SaveChanges();

            // init seed data
            var shoppingLists = new List<ShoppingList>()
            {
                new ShoppingList()
                {
                    Id = Guid.NewGuid(),
                    Name = "Rillys",
                    ShoppingItems = new List<ShoppingListItem>()
                    {
                        new ShoppingListItem()
                        {
                            ShoppingItem = new ShoppingItem()
                            {
                                Id = Guid.NewGuid(),
                                Name = "GranatÄpple",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem()
                        {
                            ShoppingItem = new ShoppingItem()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Strumpor",
                                IsBought = false
                            }
                        }
                    }
                },
                new ShoppingList()
                {
                    Id = Guid.NewGuid(),
                    Name = "Co-op Xtra",
                    ShoppingItems = new List<ShoppingListItem>()
                    {
                        new ShoppingListItem()
                        {
                            ShoppingItem = new ShoppingItem()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Glock",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem()
                        {
                            ShoppingItem = new ShoppingItem()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Walkie-talkie",
                                IsBought = false
                            }
                        }
                    }
                }
            };

            context.ShoppingLists.AddRange(shoppingLists);
            context.SaveChanges();
        }
    }
}