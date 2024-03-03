using Microsoft.EntityFrameworkCore;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShuttleInfraAPI.Repositories
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly AppDbContext _context;

        public UserProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserProjectAsync(UserProject userProject)
        {

            UserProject userInfo = new UserProject
            {
                LastUpdateDate = DateTime.UtcNow,

            };

            _context.UserProjects.Add(userProject);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserProjectAsync(int userId, int projectId)
        {
            var userProject = await _context.UserProjects
                .FirstOrDefaultAsync(up => up.UserID == userId && up.ProjectID == projectId);

            if (userProject != null)
            {
                _context.UserProjects.Remove(userProject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Projects>> GetUserProjectsAsync(int userId)
        {
            return await _context.UserProjects
                .Where(ur => ur.UserID == userId)
                .Join(
                    _context.Projects,
                    ur => ur.ProjectID,
                    r => r.id,
                    (ur, r) => r)
                .ToListAsync();
        }
    }
}
