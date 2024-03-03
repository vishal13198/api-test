using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;

namespace ShuttleInfraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {

        private readonly IResourceRepositery ResourceRepositery;

        public ResourceController(IResourceRepositery resourceRepositery)
        {
            ResourceRepositery = resourceRepositery;
        }

        [HttpGet]
        [Route("providerId/{providerId:int}/categoryId/{categoryId:int}")]
        public async Task<IActionResult> GetResources(int providerId, int categoryId)
        {

            return Ok(await ResourceRepositery.GetResources(providerId, categoryId));
        }
    }
}
