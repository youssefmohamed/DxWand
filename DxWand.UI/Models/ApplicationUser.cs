using System;
using DxWand.UI.Enums;

namespace DxWand.UI.Models
{
    public class ApplicationUser
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
        public bool IsAdmin 
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
