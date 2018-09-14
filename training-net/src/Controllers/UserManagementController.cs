using Microsoft.AspNetCore.Mvc;
using training_net.Repositories.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using training_net.Repositories;
using training_net.Models;
using training_net.Models.Views;

namespace training_net.Controllers
{
    [Route("[controller]"), Authorize]
    public class UserManagementController : Controller
    {
        private readonly DbContextOptions<DataBaseContext> _options;
        private readonly UserRepository _userRepository;

        public UserManagementController(DbContextOptions<DataBaseContext> options, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._options = options;
            this._userRepository = new UserRepository(this._options, userManager, roleManager);
        }

        [HttpGet("Users")]
        public IActionResult Users() => View(new UserManagementViewModel { Users = UserRepository.GetAllUsers() });

        public DbContextOptions<DataBaseContext> Options
        {
            get { return _options; }
        }

        public UserRepository UserRepository
        {
            get { return _userRepository; }
        }
    }
}
