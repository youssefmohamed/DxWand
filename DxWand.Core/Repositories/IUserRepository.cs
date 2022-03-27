using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DxWand.Core.Entities;
using DxWand.Core.Enums;

namespace DxWand.Core.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        Task<IReadOnlyList<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<bool> IsValidUserPassword(ApplicationUser user, string password);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<List<DateTime>> GetUsersBirthdateAsync();
        Task<List<GenderEnum>> GetUsersGenderAsync();
    }
}
