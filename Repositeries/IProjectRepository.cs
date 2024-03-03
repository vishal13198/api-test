using ShuttleInfraAPI.Models;

namespace ShuttleInfraAPI.Repositeries
{
    public interface IProjectRepository
    {
        Task<List<Projects>> GetAllProjectsAsync();
        Task<Projects> GetProjectByIdAsync(int id);
        Task<Projects> AddProjectAsync(Projects project);
        Task UpdateProjectAsync(Projects project);
        Task DeleteProjectAsync(int id);
        Task AddOrganizationProjectAsync(OrganizationProject orgProject);
        Task<List<Projects>> GetOrganizationProjectAsync(int orgId);
    }
}
