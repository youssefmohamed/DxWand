using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Core.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        Task<IReadOnlyList<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> IsValidUserPassword(ApplicationUser user, string password);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
    }
}
