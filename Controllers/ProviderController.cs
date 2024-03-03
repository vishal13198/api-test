using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;
using System;

namespace ShuttleInfraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {

        private readonly IProviderRepositery ProviderRepositery;

        public ProvidersController(IProviderRepositery providerRepositery)
        {
            ProviderRepositery = providerRepositery;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProviders()
        {
            return Ok(await ProviderRepositery.GetAllProviders());
        }

    }
}
