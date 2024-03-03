using Microsoft.AspNetCore.Mvc;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;
using ShuttleInfraAPI.Repositories;

namespace ShuttleInfraAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProjectRepository _userProjectRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUsersRepositery _usersRepository;

        public UsersController(IUserProjectRepository userProjectRepository,
                               IUserRoleRepository userRoleRepository,
                               IUsersRepositery usersRepositery)
        {
            _userProjectRepository = userProjectRepository;
            _userRoleRepository = userRoleRepository;
            _usersRepository = usersRepositery;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Server is healthy !!");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _usersRepository.GetUsers());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Users user)
        {
            try
            {
                var userId = await _usersRepository.CreateUserAsync(user);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch");
            }

            try
            {
                await _usersRepository.UpdateUserAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update user: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _usersRepository.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete user: {ex.Message}");
            }
        }


        [HttpPost("createUserProjectAndRole")]
        public async Task<IActionResult> CreateUserProjectAndRole([FromBody]UserProjectRole user)
        {
            Users userInfo = new Users
            {
                CreatedDate = DateTime.UtcNow,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                IsActive = true,
                Username = user.Username
            };
            var createdUser = await _usersRepository.CreateUserAsync(userInfo);

            if (createdUser != null && createdUser.Id.HasValue)
            {

                var userProject = new UserProject { UserID = createdUser.Id.Value, ProjectID = user.ProjectId ?? throw new InvalidOperationException("ProjectID is null"), LastUpdateDate = DateTime.UtcNow };
                var userRole = new UserRole { UserId = createdUser.Id.Value, RoleId = user.RoleId ?? throw new InvalidOperationException("RoleID is null"), LastUpdateDate = DateTime.UtcNow };


                // Add user project
                await _userProjectRepository.AddUserProjectAsync(userProject);

                // Add user role
                await _userRoleRepository.AddUserRoleAsync(userRole);

                return Ok(new { message = "User created successfully.", user = createdUser });
            }
            else
            {
                return BadRequest("Failed to create user");
            }
        }



        [HttpGet("getUsersWithRoleById/{userId}")]
        public async Task<ActionResult<UserData>> GetUserByIdWithRolesAndProjects(int userId)
        {
            UserData ud = await _usersRepository.GetUserProjectRolesDataAsync(userId);

            if (ud == null)
            {
                return NotFound("User not found");
            }
            return ud;
        }


        [HttpGet("getUsersWithRole/{roleId}")]
        public async Task<ActionResult<UserData>> GetUserWithRolesAndProjects(int roleId)
        {
            IEnumerable<UserData> userResult = await _usersRepository.GetUserWithRolesAndProjects(roleId);

            if (userResult == null)
            {
                return NotFound("User Details not found");
            }
            return Ok(userResult);
        }

        
         [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _usersRepository.GetRoles());
        }
    }
}
