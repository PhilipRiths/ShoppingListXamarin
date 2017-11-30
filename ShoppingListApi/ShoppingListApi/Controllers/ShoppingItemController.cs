using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingListApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingItem")]
    public class ShoppingItemController : Controller
    {
    }
}