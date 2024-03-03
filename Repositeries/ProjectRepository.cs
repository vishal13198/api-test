using Microsoft.EntityFrameworkCore;
using ShuttleInfraAPI.Models;

namespace ShuttleInfraAPI.Repositeries
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Projects>> GetAllProjectsAsync()
        {
           
            return await _context.Projects.ToListAsync();
        }

        public async Task<Projects> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return new Projects();
            }
            return project;
        }

        public async Task<Projects> AddProjectAsync(Projects project)
        {
            _context.Projects.Add(project);
             await _context.SaveChangesAsync();

            var result = await _context.Projects.OrderByDescending(u => u.id).FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateProjectAsync(Projects project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        


            public async Task AddOrganizationProjectAsync(OrganizationProject orgProject)
        {
            if (orgProject == null)
            {
                throw new ArgumentNullException(nameof(orgProject), "OrgID cannot be null.");
            }

            try
            {

                _context.organizations_projects.Add(orgProject);
            

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"An error occurred while adding or updating organization Project: {ex.Message}");
                throw; // Re-throw the exception to propagate it further if necessary
            }
        }


        public async Task<List<Projects>> GetOrganizationProjectAsync(int orgId)
        {
            var mappedProjects = await _context.organizations_projects
                .Where(op => op.OrgId == orgId)
                .Select(op => op.ProjectId)
                .ToListAsync();

            var projects = await _context.Projects
                .Where(p => mappedProjects.Contains(p.id))
                .ToListAsync();

            return projects;
        }
    }
}
