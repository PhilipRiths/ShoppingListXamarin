using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Entities;
using ShoppingListApi.Models;
using ShoppingListApi.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ShoppingListApi.Controllers
{
    [Authorize]
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
            var ShoppingItemsFromRepo = _shoppingItemRepository.GetAllShoppingItems();

            var shoppingItemEntity = Mapper.Map<IEnumerable<ShoppingItemDto>>(ShoppingItemsFromRepo);

            return Ok(shoppingItemEntity);
        }

        [HttpGet("{id}", Name = "GetShoppingItem")]
        public IActionResult GetShoppingItem(Guid id)
        {
            var shoppingItemFromRepo = _shoppingItemRepository.GetShoppingItem(id);

            var shoppingItemEntity = Mapper.Map<ShoppingItemDto>(shoppingItemFromRepo);

            return Ok(shoppingItemEntity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingItem(Guid id)
        {
            var ShoppingItemFromRepo = _shoppingItemRepository.GetShoppingItem(id);
            if (ShoppingItemFromRepo == null)
            {
                return NotFound();
            }

            _shoppingItemRepository.DeleteShoppingItem(ShoppingItemFromRepo);
            _shoppingItemRepository.DeleteShoppingListItemContainingShoppingItem(id);

            if (!_shoppingItemRepository.Save())
            {
                throw new Exception($"Deleting ShoppingItem on {id} failed on save");
            }

            return NoContent();
        }

        [HttpPost(Name = "CreateShoppingItem")]
        public IActionResult CreateShoppingItem([FromBody] ShoppingItemForCreationDto shoppingItem)
        {
            if (shoppingItem == null)
            {
                return NotFound();
            }

            var shoppingItemEntity = Mapper.Map<ShoppingItem>(shoppingItem);

            _shoppingItemRepository.AddShoppingItem(shoppingItemEntity);

            if (!_shoppingItemRepository.Save())
            {
                throw new Exception("Creating an shoppingItem failed on save.");
            }

            var shoppingItemToReturn = Mapper.Map<ShoppingItemDto>(shoppingItemEntity);

            return CreatedAtRoute("GetShoppingItem",
                new { id = shoppingItemToReturn.Id },
                shoppingItemToReturn);
        }

        [HttpPatch()]
        public IActionResult PartiallyUpdateShoppingItem([FromBody] ShoppingItemForEditDto shoppingItem)
        {
            if (shoppingItem == null)
            {
                return NotFound();
            }

            var shoppingItemEntity = Mapper.Map<ShoppingItem>(shoppingItem);

            _shoppingItemRepository.EditShoppingItem(shoppingItemEntity);

            if (!_shoppingItemRepository.Save())
            {
                throw new Exception($"Updating shoppingItem on {shoppingItem.Id} failed on save");
            }

            return NoContent();
        }
    }
}