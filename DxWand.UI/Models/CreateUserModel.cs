using System;
using System.ComponentModel.DataAnnotations;
using DxWand.UI.Enums;

namespace DxWand.UI.Models
{
    public class CreateUserModel
    {
        [Required]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }
        [Required]
        public string Password 
        {
            get; 
            set; 
        }
        [Required]
        public DateTime BirthDate
        {
            get;
            set;
        }
        [Required]
        public GenderEnum Gender
        {
            get;
            set;
        }
    }
}
