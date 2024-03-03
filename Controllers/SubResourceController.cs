using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuttleInfraAPI.Repositeries;

namespace ShuttleInfraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubResourceController : ControllerBase
    {

        private readonly IResourceRepositery ResourceRepositery;

        public SubResourceController(IResourceRepositery resourceRepositery)
        {
            ResourceRepositery = resourceRepositery;
        }

        [HttpGet]
        [Route("resourceId/{resourceId:int}")]
        public async Task<IActionResult> GetSubResources(int resourceId)
        {
            return Ok(await ResourceRepositery.GetSubResources(resourceId));
        }
    }
}
