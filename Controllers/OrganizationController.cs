using Microsoft.AspNetCore.Mvc;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationsController(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }




    [HttpGet]
    public async Task<IActionResult> GetOrganizations()
    {
        return Ok(await _organizationRepository.GetAllOrganizationsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Organizations>> GetOrganization(int id)
    {
        try
        {
            var organization = await _organizationRepository.GetOrganizationsByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return Ok(organization);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
       
    }


    [HttpPost("CreateOrganization")]
    public async Task<ActionResult<Organizations>> CreateOrganization([FromBody] Organizations organization)
    {
        try
        {
            if (organization == null)
            {
                return BadRequest();
            }

            var organizationId = await _organizationRepository.AddOrganizationsAsync(organization);

            return CreatedAtAction(nameof(GetOrganizations), new { id = organizationId }, organization);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(int id, Organizations organization)
    {
        if (id != organization.Id)
        {
            return BadRequest();
        }
        await _organizationRepository.UpdateOrganizationsAsync(organization);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(int id)
    {
        await _organizationRepository.DeleteOrganizationsAsync(id);
        return NoContent();
    }
}
