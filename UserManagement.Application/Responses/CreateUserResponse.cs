using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Enums;

namespace UserManagement.Application.Responses
{
    public class CreateUserResponse
    {
        public string Email 
        {
            get; 
            set; 
        }
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
