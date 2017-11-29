using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingListApi.Models;
using ShoppingListApi.Services;
using System.Collections.Generic;

namespace ShoppingListApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingList")]
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
    }
}