using System;
using DxWand.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace DxWand.Core.Entities
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
