using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using training_net.Models;
using training_net.Repositories.Database;
using training_net.Repositories.Interfaces;

namespace training_net.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbContextOptions<DataBaseContext> _options;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(DbContextOptions<DataBaseContext> options, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._options = options;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<User> GetUserById(string id)
        {
            return await UserManager.FindByIdAsync(id);
        }

        public List<User> GetAllUsers()
        {
            return UserManager.Users.ToList();
        }

        public int GetUserId(ClaimsPrincipal user) 
        {
            var userId = UserManager.GetUserId(user);
            return Convert.ToInt32(userId);
        }

        public UserManager<User> UserManager
        {
            get { return _userManager; }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get { return _roleManager; }
        }

        public DataBaseContext Context
        {
            get { return new DataBaseContext(_options); }
        }
    }
}
