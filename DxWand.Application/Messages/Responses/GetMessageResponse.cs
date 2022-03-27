using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxWand.Application.Messages.Responses
{
    public class GetMessageResponse
    {
        public string Lang 
        {
            get; 
            set; 
        }
        public string Content 
        {
            get; 
            set;
        }
    }
}
