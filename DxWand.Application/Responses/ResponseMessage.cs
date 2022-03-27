using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxWand.Application.Responses
{
    public class ResponseMessage<T>  where T : class
    {
        public bool IsSuccess 
        {
            get; 
            set;
        }
        public string Message 
        {
            get; 
            set; 
        }
        public T Data 
        {
            get; 
            set;
        }
        public int StatusCode 
        {
            get;
            set;
        }
    }
}
