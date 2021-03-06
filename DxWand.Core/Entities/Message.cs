using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxWand.Core.Entities
{
    public class Message
    {
        public int Id 
        {
            get;
            set; 
        }
        public string Content 
        {
            get; 
            set; 
        }
        public string Lang 
        {
            get; 
            set;
        }
        public string UserId 
        {
            get; 
            set; 
        }
        public ApplicationUser User 
        {
            get;
            set; 
        }
    }
}
