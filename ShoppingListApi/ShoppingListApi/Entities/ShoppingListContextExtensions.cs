using ShoppingListApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListApi.Entities
{
    public static class ShoppingListContextExtensions
    {
        public static void EnsureSeedDataForContext(this ShoppingListContext context)
        {
            if (context.Users.Any() || context.ShoppingLists.Any())
            {
                return;
            }

            //context.Users.RemoveRange(context.Users);
            //context.ShoppingItems.RemoveRange(context.ShoppingItems);
            //context.ShoppingLists.RemoveRange(context.ShoppingLists);
            //context.SaveChanges();

            // init seed data

            var users = new List<User>
            {
                new User
                {
                    FirstName = "Redige",
                    LastName = "Redginsson",
                    Mail = "RedigeRedginsson@redigmail.org",
                    GoogleId = "4561203165113013546"
                },
                new User
                {
                    FirstName = "Redi",
                    LastName = "Redgi",
                    Mail = "RediRedgi@redimail.org",
                    GoogleId = "1563587315654646465"
                }
            };

            var shoppingItems = new List<ShoppingItem>
            {
                new ShoppingItem
                {
                    Name = "OutsideListItem",
                    IsBought = true
                },
                new ShoppingItem
                {
                    Name = "Test1",
                    IsBought = true
                },
                new ShoppingItem
                {
                    Name = "Test3",
                    IsBought = true
                },
                new ShoppingItem
                {
                    Name = "Test3",
                    IsBought = true
                },
            };

            var shoppingLists = new List<ShoppingList>
            {
                new ShoppingList
                {
                    Name = "Rillys",
                    ShoppingItems = new List<ShoppingListItem>
                    {
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Name = "GranatÄpple",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Name = "Strumpor",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem
                        {
                            ShoppingItem = shoppingItems.Find(i => i.Name == "Test1")
                        }
                    },
                    LastEditedBy = users.Find(u => u.Mail == "RedigeRedginsson@redigmail.org"),
                    LastEdited = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    CreatedBy = users.Find(u => u.Mail == "RedigeRedginsson@redigmail.org"),
                    Users = new List<ShoppingListUser>
                    {
                        new ShoppingListUser
                        {
                            User = users.Find(u => u.Mail == "RedigeRedginsson@redigmail.org"),
                            UserId = users.Find(u => u.Mail == "RedigeRedginsson@redigmail.org").Id
                        },
                        new ShoppingListUser
                        {
                            User = users.Find(u => u.Mail == "RediRedgi@redimail.org"),
                            UserId = users.Find(u => u.Mail == "RediRedgi@redimail.org").Id
                        }
                    }
                },
                new ShoppingList
                {
                    Name = "Co-op Xtra",
                    ShoppingItems = new List<ShoppingListItem>
                    {
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Name = "Glock",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Name = "Walkie-talkie",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem
                        {
                            ShoppingItem = shoppingItems.Find(i => i.Name == "Test1")
                        }
                    },
                    LastEditedBy = users.Find(u => u.Mail == "RediRedgi@redimail.org"),
                    LastEdited = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    CreatedBy = users.Find(u => u.Mail == "RediRedgi@redimail.org")
                }
            };

            context.Users.AddRange(users);
            context.ShoppingItems.AddRange(shoppingItems);
            context.ShoppingLists.AddRange(shoppingLists);
            context.SaveChanges();
        }
    }
}