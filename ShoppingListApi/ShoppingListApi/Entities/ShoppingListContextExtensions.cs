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
            context.ShoppingItems.RemoveRange(context.ShoppingItems);
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();

            // init seed data
            var shoppingLists = new List<ShoppingList>
            {
                new ShoppingList
                {
                    Id = new Guid("62880c0f-1e62-48ce-bd62-94f0420765b2"),
                    Name = "Rillys",
                    ShoppingItems = new List<ShoppingListItem>
                    {
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Id = new Guid("c343d725-d2fc-4772-8873-60ba9754ebcb"),
                                Name = "GranatÄpple",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Id = new Guid("7338f677-eb35-40a6-8834-b34bfa5bb3b5"),
                                Name = "Strumpor",
                                IsBought = false
                            }
                        }
                    },
                    LastEdited = new DateTime(2000, 10, 10),
                    CreatedBy = new User
                    {
                        Id = new Guid("aee07c9e-da35-4a65-8f28-bffc004081b6"),
                        FirstName = "Redige",
                        LastName = "Redginsson",
                        Mail = "RedigeRedginsson@redigmail.org"
                    }
                },
                new ShoppingList
                {
                    Id = new Guid("a13b4a16-5fe6-4978-8510-991d24f47c25"),
                    Name = "Co-op Xtra",
                    ShoppingItems = new List<ShoppingListItem>
                    {
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Id = new Guid("f86f1744-c77e-4752-a68b-b790bf4c026a"),
                                Name = "Glock",
                                IsBought = false
                            }
                        },
                        new ShoppingListItem
                        {
                            ShoppingItem = new ShoppingItem
                            {
                                Id = new Guid("4443edda-0849-4453-a60e-0085e4fd8dbb"),
                                Name = "Walkie-talkie",
                                IsBought = false
                            }
                        }
                    },
                    LastEdited = new DateTime(2001, 11, 11),
                    CreatedBy = new User
                    {
                        Id = new Guid("b1cdafb9-db7e-485d-b89b-216fb71664e5"),
                        FirstName = "Redi",
                        LastName = "Redgi",
                        Mail = "RediRedgi@redimail.org"
                    }
                }
            };

            context.ShoppingLists.AddRange(shoppingLists);
            context.SaveChanges();
        }
    }
}