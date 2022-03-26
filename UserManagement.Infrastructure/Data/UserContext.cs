using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;

namespace UserManagement.Infrastructure.Data
{
    public class UserContext 
        : IdentityDbContext<ApplicationUser>
    {
        public UserContext(DbContextOptions<UserContext> options) 
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SeedRoles();
            builder.SeedAdminUser();
            builder.SeedUserRoles(); 
        }
    }
}
