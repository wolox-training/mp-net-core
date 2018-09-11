using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using training_net.Models;

namespace training_net.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserById(string id);
        List<User> GetAllUsers();
        int GetUserId(ClaimsPrincipal user);
    }
}
