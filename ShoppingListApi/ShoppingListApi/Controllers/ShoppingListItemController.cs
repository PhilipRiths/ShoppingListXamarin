using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Models;
using ShoppingListApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingListApi.Controllers
{
    [Authorize]
    [Route("api/ShoppingListItem")]
    public class ShoppingListItemController : Controller
    {
        private IShoppingListItemRepository _shoppingListItemRepository;

        public ShoppingListItemController(IShoppingListItemRepository shoppingListItemRepository)
        {
            _shoppingListItemRepository = shoppingListItemRepository;
        }

        [HttpGet()]
        public IActionResult GetAllShoppingListsAndItems()
        {
            var allItemsFromRepo = _shoppingListItemRepository.GetAllShoppingListsAndItems();

            var allItems = Mapper.Map<IEnumerable<ShoppingListItemDto>>(allItemsFromRepo);

            return Ok(allItems);
        }

        [HttpGet("listItems/{shoppingListId}")]
        public IActionResult GetAllItemsOnShoppingList(int shoppingListId)
        {
            if (!_shoppingListItemRepository.ShoppingListExists(shoppingListId))
            {
                return NotFound();
            }

            var itemsForShoppingListFromRepo = _shoppingListItemRepository.GetShoppingListsAndItemsByListId(shoppingListId);

            var itemsForShoppingList = Mapper.Map<IEnumerable<ShoppingListItemDto>>(itemsForShoppingListFromRepo);

            var listOfItems = itemsForShoppingList.Select(sl => sl.ShoppingItem).ToList();

            return Ok(listOfItems);
        }

        [HttpGet("itemLists/{shoppingItemId}")]
        public IActionResult GetAllListsOnShoppingItem(int shoppingItemId)
        {
            if (!_shoppingListItemRepository.ShoppingItemExists(shoppingItemId))
            {
                return NotFound();
            }

            var listsForShoppingItemFromRepo = _shoppingListItemRepository.GetShoppingListsAndItemsByItemId(shoppingItemId);

            var listsForShoppingItem = Mapper.Map<IEnumerable<ShoppingListItemDto>>(listsForShoppingItemFromRepo);

            var listOfLists = listsForShoppingItem.Select(sl => sl.ShoppingList).ToList();

            return Ok(listOfLists);
        }
    }
}