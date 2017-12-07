using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShoppingListApi.Services;
using AutoMapper;
using ShoppingListApi.Models;
using ShoppingListApi.Entities;

namespace ShoppingListApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/ShoppingListUser")]
    public class ShoppingListUserController : Controller
    {
        private IShoppingListUserRepository _shoppingListUserRepository;

        public ShoppingListUserController(IShoppingListUserRepository shoppingListUserRepository)
        {
            _shoppingListUserRepository = shoppingListUserRepository;
        }

        [HttpGet()]
        public IActionResult GetShoppingListUsers()
        {
            var shoppingListUsersFromRepo = _shoppingListUserRepository.GetShoppingListUsers();

            var shoppingListUsers = Mapper.Map<IEnumerable<ShoppingListUserDto>>(shoppingListUsersFromRepo);

            return Ok(shoppingListUsers); // Why use Ok here?
        }

        [HttpGet("{id}")]
        public IActionResult GetShoppingListUser(string googleId)
        {
            if (_shoppingListUserRepository.ShoppingListUserExists(googleId))
            {
                return NotFound();
            }

            var shoppingListUserFromRepo = _shoppingListUserRepository.GetShoppingListUser(googleId);

            var shoppingListUser = Mapper.Map<ShoppingListUserDto>(shoppingListUserFromRepo);

            return new JsonResult(shoppingListUser); // Why use JsonResult here?
        }

        [HttpPost("CreateShoppingListUser")]
        public IActionResult CreateShoppingListUser([FromBody] ShoppingUserForCreationDto shoppingListUser) // Should I create this Dto?
        {
            if (shoppingListUser == null)
            {
                return NotFound();
            }

            var shoppingListUserEntity = Mapper.Map<ShoppingListUser>(shoppingListUser);

            _shoppingListUserRepository.AddShoppingListUser(shoppingListUserEntity);

            if (!_shoppingListUserRepository.Save())
            {
                throw new Exception("Creating a ShoppingListUser failed on save.");
            }

            var shoppingListUserToReturn = Mapper.Map<ShoppingListUserDto>(shoppingListUserEntity);

            return Ok(shoppingListUserToReturn);
        }
    }
}