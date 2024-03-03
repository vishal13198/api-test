using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrganizationRepository 
{

    Task<List<Organizations>> GetAllOrganizationsAsync();
    Task<Organizations> GetOrganizationsByIdAsync(int id);
    Task<int> AddOrganizationsAsync(Organizations organization);
    Task UpdateOrganizationsAsync(Organizations organization);
    Task DeleteOrganizationsAsync(int id);
}
