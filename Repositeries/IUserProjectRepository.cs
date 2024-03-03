using ShuttleInfraAPI.Models;

namespace ShuttleInfraAPI.Repositeries
{
    public interface IUserProjectRepository
    {

        Task AddUserProjectAsync(UserProject userProject);
        Task RemoveUserProjectAsync(int userId, int projectId);
       Task<IEnumerable<Projects>> GetUserProjectsAsync(int userId);


    }
}
