using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ShuttleInfraAPI.Models;

namespace ShuttleInfraAPI.Repositeries
{
    public class UsersRepositery : IUsersRepositery
    {
        private readonly AppDbContext _context;

        public UsersRepositery(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Users>> GetUsers()
        {
            return await _context.users.ToListAsync();
        }
        public async Task<Users> CreateUserAsync(Users user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User object cannot be null");
                }

                _context.users.Add(user);
                await _context.SaveChangesAsync();

                var createdUser = await _context.users.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
                if (createdUser == null)
                {
                    throw new Exception("Failed to retrieve the created user.");
                }

                return createdUser;
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505" && pgEx.Message.Contains("users_email_key"))
                    {
                        // Duplicate email error
                        throw new Exception("Email address already exists.");
                    }
                    else if (pgEx.SqlState == "23505" && pgEx.Message.Contains("users_username_key"))
                    {
                        // Duplicate username error
                        throw new Exception("Username already exists.");
                    }
                }

                // For other database update errors
                throw new Exception("An unexpected error occurred while creating the user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }



        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }

        public async Task UpdateUserAsync(Users user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }


        public async Task AddOrUpdateUserProjectAsync(UserProject userProject)
        {
            if (userProject == null)
            {
                throw new ArgumentNullException(nameof(userProject), "User project cannot be null.");
            }

            try
            {
                var existingUserProject = await _context.UserProjects.FirstOrDefaultAsync(up => up.UserID == userProject.UserID && up.ProjectID == userProject.ProjectID);

                if (existingUserProject != null)
                {
                    // Update existing user project
                    existingUserProject.IsActive = userProject.IsActive;
                    existingUserProject.LastUpdateDate = userProject.LastUpdateDate;
                }
                else
                {
                    // Add new user project
                    _context.UserProjects.Add(userProject);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"An error occurred while adding or updating user project: {ex.Message}");
                throw; // Re-throw the exception to propagate it further if necessary
            }
        }


        public async Task AddOrUpdateUserRoleAsync(UserRole userRole)
        {
            if (userRole == null)
            {
                throw new ArgumentNullException(nameof(userRole), "User role cannot be null.");
            }

            try
            {
                var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId);

                if (existingUserRole != null)
                {
                    // Update existing user role
                    existingUserRole.IsActive = userRole.IsActive;
                    existingUserRole.LastUpdateDate = userRole.LastUpdateDate;
                }
                else
                {
                    // Add new user role
                    _context.UserRoles.Add(userRole);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"An error occurred while adding or updating user role: {ex.Message}");
                throw; // Re-throw the exception to propagate it further if necessary
            }
        }


        public async Task<UserData> GetUserProjectRolesDataAsync(int userId)
        {
            try
            {
                var userData = await _context.users
                    .Where(u => u.Id == userId)
                    .Join(_context.UserRoles,
                        u => u.Id,
                        ur => ur.UserId,
                        (u, ur) => new { User = u, UserRole = ur })
                    .Join(_context.Roles,
                        ur => ur.UserRole.RoleId,
                        r => r.id,
                        (ur, r) => new { ur.User, ur.UserRole, Role = r })
                    .Join(_context.UserProjects,
                        ur => ur.User.Id,
                        up => up.UserID,
                        (ur, up) => new { ur.User, ur.UserRole, ur.Role, UserProject = up })
                    .Join(_context.Projects,
                        up => up.UserProject.id,
                        p => p.id,
                        (up, p) => new UserData
                        {
                            UserID = (int)up.User.Id,
                            Username = up.User.Username,
                            UserEmail = up.User.Email,
                            RoleID = up.Role.id,
                            RoleName = up.Role.RoleName,
                            ProjectID = up.UserProject.id,
                            ProjectName = p.ProjectName
                        })
                    .FirstOrDefaultAsync();

                // Check if userData is null
                if (userData == null)
                {
                    // Return a default or empty UserData object
                    return new UserData(); // You can customize this as needed
                }

                return userData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user project roles data: {ex.Message}");
                throw; // Re-throw the exception to propagate it further if necessary
            }

        }


        public async Task<IEnumerable<UserData>> GetUserWithRolesAndProjects(int roleId)
        {
            try
            {
                var userData = await _context.users
                    .Join(_context.UserRoles,
                        u => u.Id,
                        ur => ur.UserId,
                        (u, ur) => new { User = u, UserRole = ur }).ToListAsync();


                List<UserData> userList = (from org in _context.organizations_projects
                                           where org.OrgId == roleId
                                           join proj in _context.Projects on org.ProjectId equals proj.id
                            join userProj in _context.UserProjects on proj.id equals userProj.ProjectID into userProjects
                            from userProject in userProjects.DefaultIfEmpty() 
                            join user in _context.users on userProject.UserID equals user.Id
                            join userRole in _context.UserRoles on user.Id equals userRole.UserId into userRolesList
                              from userRol in userRolesList.DefaultIfEmpty()
                            join role in _context.Roles on userRol.RoleId equals role.id
                              select new UserData
                            {
                                UserID = user.Id ?? 0,
                                Username = user.Username ?? "",
                                UserEmail = user.Email ?? "",
                                RoleID = role.id,
                                RoleName = role.RoleName ?? "",
                                ProjectID = proj.id,
                                ProjectName = proj.ProjectName ?? ""
                            }).ToList();

                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user data: {ex.Message}");
                throw; // Re-throw the exception to propagate it further if necessary
            }
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }


    }
}
