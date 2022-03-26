using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;
using UserManagement.Core.Enums;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserContext _userContext;

        public UserRepository(UserManager<ApplicationUser> userManager, UserContext userContext)
        {
            _userManager = userManager;
            _userContext = userContext;
        }
        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                return null;

            var addedUser = await GetByEmailAsync(user.Email);

            var role = UserRolesEnum.User.ToString();
            await _userManager.AddToRoleAsync(addedUser, role);

            return addedUser;
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync()
        {
            return await _userContext.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return (List<string>) await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsValidUserPassword(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
