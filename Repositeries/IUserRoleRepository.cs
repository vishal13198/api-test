using ShuttleInfraAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShuttleInfraAPI.Repositories
{
    public interface IUserRoleRepository
    {
        Task AddUserRoleAsync(UserRole userRole);
        Task RemoveUserRoleAsync(int userId, int roleId);
        Task<IEnumerable<Role>> GetUserRolesAsync(int userId);
    }
}
