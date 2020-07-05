using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService : IAppService<BlogUser, User>
    {
        void Update(User user, IEnumerable<string> roles, IEnumerable<string> userClaims);
        UserRolesClaims GetProfile(string email, string password);
        UserRolesClaims GetProfile(string userId);
        Task<UserRolesClaims> RegisterUser(User user, string password);
        IEnumerable<UserInfo> GetBloggers();
        // Admin mode
        Task<UserRolesClaims> RegisterUserWithPermission(User user, string password, IEnumerable<string> roles, IEnumerable<string> userClaims, string supervisorId);
        IEnumerable<User> GetUsersBySupervisorId(string supervisorId);
    }
}
