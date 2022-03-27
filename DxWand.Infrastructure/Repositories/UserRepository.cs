using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DxWand.Core.Entities;
using DxWand.Core.Enums;
using DxWand.Core.Repositories;
using DxWand.Infrastructure.Data;
using System.Linq;

namespace DxWand.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (!result.Succeeded)
                return null;

            var addedUser = await GetByEmailAsync(user.Email);

            var role = UserRolesEnum.User.ToString();
            await _userManager.AddToRoleAsync(addedUser, role);

            return addedUser;
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return (List<string>) await _userManager.GetRolesAsync(user);
        }

        public async Task<List<System.DateTime>> GetUsersBirthdateAsync()
        {
            return await _userManager.Users.Select(x => x.BirthDate).ToListAsync();
        }

        public async Task<List<GenderEnum>> GetUsersGenderAsync()
        {
            return await _userManager.Users.Select(x => x.Gender).ToListAsync();
        }

        public async Task<bool> IsValidUserPassword(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
