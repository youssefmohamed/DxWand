using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Core.Enums;

namespace DxWand.Application.Users.Responses
{
    public class GetUserResponse
    {
        public string Id 
        {
            get; 
            set; 
        }
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
