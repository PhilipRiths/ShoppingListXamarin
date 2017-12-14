﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingListApi.Entities;
using ShoppingListApi.Models;
using ShoppingListApi.Services;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Controllers
{
    [Route("api/ShoppingLists")]
    public class ShoppingListController : Controller
    {
        private IShoppingListRepository _shoppingListRepository;
        private ILogger<ShoppingListController> _logger;
        // private IShoppingListHub _shoppingListHub;

        public ShoppingListController(IShoppingListRepository shoppingListRepository,
            ILogger<ShoppingListController> logger
            // IShoppingListHub shoppingListHub
            )
        {
            _shoppingListRepository = shoppingListRepository;
            _logger = logger;
            // _shoppingListHub = shoppingListHub;
        }

        [HttpGet("Test")]
        public IActionResult TestMethod()
        {
            return Ok("This is totally a JSON string, like, totally...");
        }

        [HttpGet()]
        public IActionResult GetShoppingLists()
        {
            var shoppingListsFromRepo = _shoppingListRepository.GetShoppingLists();

            var shoppingLists = Mapper.Map<IEnumerable<ShoppingListDto>>(shoppingListsFromRepo);

            return Ok(shoppingLists);
        }

        [HttpGet("{id}", Name = "GetShoppingList")]
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

        [HttpPost()]
        public IActionResult CreateShoppingList([FromBody] ShoppingListForCreationDto shoppingList)
        {
            if (shoppingList == null)
            {
                return BadRequest();
            }

            var shoppingListEntity = Mapper.Map<ShoppingList>(shoppingList);

            _shoppingListRepository.AddShoppingList(shoppingListEntity);

            if (!_shoppingListRepository.Save())
            {
                throw new Exception("Creating an shoppingList failed on save.");
            }

            // _shoppingListHub.AddShoppingList(shoppingListEntity);

            var shoppingListToReturn = Mapper.Map<ShoppingListDto>(shoppingListEntity);

            return CreatedAtRoute("GetShoppingList",
                new { id = shoppingListToReturn.Id },
                shoppingListToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingList(Guid id)
        {
            var shoppingListFromRepo = _shoppingListRepository.GetShoppingList(id);
            if (shoppingListFromRepo == null)
            {
                return NotFound();
            }

            _shoppingListRepository.DeleteShoppingList(shoppingListFromRepo);
            _shoppingListRepository.DeleteShoppingListItemContainingShoppingList(id);

            if (!_shoppingListRepository.Save())
            {
                throw new Exception($"Deleting shoppingList on {id} failed on save");
            }

            _logger.LogInformation(100, $"ShoppingList {id} was deleted.");

            return NoContent();
        }

        [HttpPatch()]
        public IActionResult PartiallyUpdateShoppingList([FromBody] ShoppingListForEditDto shoppingList)
        {
            if (shoppingList == null)
            {
                return NotFound();
            }

            var shoppingListEntity = Mapper.Map<ShoppingList>(shoppingList);

            _shoppingListRepository.EditShoppingList(shoppingListEntity);

            if (!_shoppingListRepository.Save())
            {
                throw new Exception($"Updating shoppingList on {shoppingList.Id} failed on save");
            }

            // await _shoppingListHub.UpdateProduct(shoppingListEntity);

            return NoContent();
        }
    }
}