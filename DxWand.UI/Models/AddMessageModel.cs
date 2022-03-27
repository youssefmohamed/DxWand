using System.ComponentModel.DataAnnotations;

namespace DxWand.UI.Models
{
    public class AddMessageModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s@]*$")]
        public string Content 
        {
            get; 
            set;
        }
    }
}
