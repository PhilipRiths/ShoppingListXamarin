using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Entities;
using ShoppingListApi.Models;
using ShoppingListApi.Services;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingListItems")]
    public class ShoppingListItemController : Controller
    {
        private IShoppingListItemRepository _shoppingListItemRepository;

        public ShoppingListItemController(IShoppingListItemRepository shoppingListItemRepository)
        {
            _shoppingListItemRepository = shoppingListItemRepository;
        }

        [HttpGet()]
        public IActionResult GetAllShoppingListItems()
        {
            var allItemsFromRepo = _shoppingListItemRepository.GetAllShoppingListItems();

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
            if (!_shoppingListItemRepository.ShoppingListExists(shoppingListId))
            {
                return NotFound();
            }

            var itemsForShoppingListFromRepo = _shoppingListItemRepository.GetShoppingListItems(shoppingListId);

            var itemsForShoppingList = Mapper.Map<IEnumerable<ShoppingListItemDto>>(itemsForShoppingListFromRepo);

            return new JsonResult(itemsForShoppingList);
        }

        [HttpPatch()]
        public IActionResult PartiallyUpdateShoppingListItem([FromBody] ShoppingItemForEditDto shoppingItem)
        {
            if (shoppingItem == null)
            {
                return NotFound();
            }

            var shoppingItemEntity = Mapper.Map<ShoppingItem>(shoppingItem);

            _shoppingListItemRepository.EditShoppingItem(shoppingItemEntity);

            if (!_shoppingListItemRepository.Save())
            {
                throw new Exception($"Updating shoppingItem on {shoppingItem.Id} failed on save");
            }

            return NoContent();
        }
    }
}