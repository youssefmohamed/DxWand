using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserManagement.Core.Enums;

namespace UserManagement.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime BirthDate 
        {
            get; 
            set; 
        }

        public GenderEnum Gender 
        {
            get; 
            set; 
        }
       
    }
}
