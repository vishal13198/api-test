using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositories;

namespace ShuttleInfraAPI.Repositeries
{
    public interface IOrganizationProjectRepository : IRepository<OrganizationProject>
    {
        Task<List<OrganizationProject>> GetProjectsByOrgIdAsync(int orgId);
    }
}
