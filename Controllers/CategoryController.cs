using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuttleInfraAPI.Repositeries;

namespace ShuttleInfraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IResourceRepositery ResourceRepositery;

        public CategoryController(IResourceRepositery resourceRepositery)
        {
            ResourceRepositery = resourceRepositery;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await ResourceRepositery.GetCategories());
        }

    }
}
