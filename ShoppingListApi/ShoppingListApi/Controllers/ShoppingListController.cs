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
    [Route("api/ShoppingLists")]
    public class ShoppingListController : Controller
    {
        private IShoppingListRepository _shoppingListRepository;

        public ShoppingListController(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        [HttpGet()]
        public IActionResult GetShoppingLists()
        {
            var shoppingListsFromRepo = _shoppingListRepository.GetShoppingLists();

            var shoppingLists = Mapper.Map<IEnumerable<ShoppingListDto>>(shoppingListsFromRepo);

            return Ok(shoppingLists);
        }

        [HttpGet("{id}")]
        public IActionResult GetShoppingList(Guid id)
        {
            var shoppingListFromRepo = _shoppingListRepository.GetShoppingList(id);

            var shoppingList = Mapper.Map<ShoppingListDto>(shoppingListFromRepo);
            return new JsonResult(shoppingList);
        }

        [HttpPost()]
        public IActionResult AddShoppingList(ShoppingList shoppingList)
        {
            if (shoppingList == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete()]
        public IActionResult DeleteShoppingList(Guid shoppingListId)
        {
            return Ok();
        }

        [HttpPatch()]
        public IActionResult PartiallyUpdateShoppingList()
        {
            return Ok();
        }
    }
}