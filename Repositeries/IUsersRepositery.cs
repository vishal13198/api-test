using ShuttleInfraAPI.Models;

namespace ShuttleInfraAPI.Repositeries
{
    public interface IUsersRepositery
    {
        Task<List<Users>> GetUsers();
        Task<Users> CreateUserAsync(Users user);
        Task<Users> GetUserByIdAsync(int id);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);
        Task AddOrUpdateUserProjectAsync(UserProject userProject);

        Task AddOrUpdateUserRoleAsync(UserRole userRole);
        Task<UserData> GetUserProjectRolesDataAsync(int id);
        Task<IEnumerable<UserData>> GetUserWithRolesAndProjects(int roleId);

        Task<List<Role>> GetRoles();


    }
}
