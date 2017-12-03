using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Models;
using ShoppingListApi.Services;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingItems")]
    public class ShoppingItemController : Controller
    {
        private IShoppingItemRepository _shoppingItemRepository;

        public ShoppingItemController(IShoppingItemRepository shoppingItemRepository)
        {
            _shoppingItemRepository = shoppingItemRepository;
        }

        [HttpGet()]
        public IActionResult GetAllShoppingItems()
        {
            var allItemsFromRepo = _shoppingItemRepository.GetAllShoppingListItems();

            var allItems = Mapper.Map<IEnumerable<ShoppingListItemDto>>(allItemsFromRepo);

            //foreach (var item in allItems)
            //{
            //    item.ShoppingItem = Mapper.Map<ShoppingItem>(item.ShoppingItem);
            //    item.ShoppingList = Mapper.Map<ShoppingList>(item.ShoppingList);
            //}

            return Ok(allItems);
        }

        [HttpGet("{shoppingListId}")]
        public IActionResult GetAllItemsOnShoppingList(Guid shoppingListId)
        {
            if (!_shoppingItemRepository.ShoppingListExists(shoppingListId))
            {
                return NotFound();
            }

            var itemsForShoppingListFromRepo = _shoppingItemRepository.GetShoppingListItem(shoppingListId);

            var itemsForShoppingList = Mapper.Map<IEnumerable<ShoppingListItemDto>>(itemsForShoppingListFromRepo);

            return new JsonResult(itemsForShoppingList);
        }
    }
}