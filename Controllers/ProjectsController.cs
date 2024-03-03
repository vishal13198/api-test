using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;
using ShuttleInfraAPI.Repositories;

namespace ShuttleInfraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projects>>> GetProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Projects>> GetProject(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpPost("CreateProject")]
        public async Task<ActionResult<Projects>> CreateProject([FromBody] OrgMappProjects project)
        {

            try
            {
                if (project == null)
                {
                    return BadRequest();
                }
                Projects projInfo = new Projects
                {
                    StartDate = DateTime.UtcNow,
                    ProjectName = project.ProjectName,
                    ProjectDescription = project.ProjectDescription,
                    IsActive = true,
                };

                var createdProject = await _projectRepository.AddProjectAsync(projInfo);

                if (createdProject != null)
                {

                    var orgProject = new OrganizationProject { ProjectId = createdProject.id, OrgId = project.OrgId ?? throw new InvalidOperationException("OrgId is null"), LastUpdateDate = DateTime.UtcNow };

                    await _projectRepository.AddOrganizationProjectAsync(orgProject);

                    return Ok(new { message = "Project created successfully.", project = createdProject });
                }
                else
                {
                    return BadRequest("Failed to create user");
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Projects project)
        {
            if (id != project.id)
            {
                return BadRequest();
            }

            try
            {
                await _projectRepository.UpdateProjectAsync(project);
            }
            catch (Exception)
            {
                if (!await ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            await _projectRepository.DeleteProjectAsync(id);

            return NoContent();
        }

        private async Task<bool> ProjectExists(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            return project != null;
        }


        [HttpGet("GetOrganizationProjectAsync")]
        public async Task<ActionResult<List<Projects>>> GetOrganizationProjectAsync(int orgId)
        {
            var project = await _projectRepository.GetOrganizationProjectAsync(orgId);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }
    }
}
