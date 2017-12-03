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
            if (!_shoppingListRepository.ShoppingListExists(id))
            {
                return NotFound();
            }

            var shoppingListFromRepo = _shoppingListRepository.GetShoppingList(id);

            var shoppingList = Mapper.Map<ShoppingListDto>(shoppingListFromRepo);

            return new JsonResult(shoppingList);
        }

        [HttpPost(Name = "CreateShoppingList")]
        public IActionResult CreateShoppingList([FromBody] ShoppingListForCreationDto shoppingList)
        {
            if (shoppingList == null)
            {
                return NotFound();
            }

            var shoppingListEntity = Mapper.Map<ShoppingList>(shoppingList);

            _shoppingListRepository.AddShoppingList(shoppingListEntity);

            if (!_shoppingListRepository.Save())
            {
                throw new Exception("Creating an author failed on save.");
            }

            var shoppingListToReturn = Mapper.Map<ShoppingListDto>(shoppingListEntity);

            return Ok(shoppingListToReturn);
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