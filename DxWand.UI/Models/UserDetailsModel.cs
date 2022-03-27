using System.Collections.Generic;

namespace DxWand.UI.Models
{
    public class UserDetailsModel
    {
        public ApplicationUser ApplicationUser 
        {
            get;
            set; 
        }
        public List<MessageModel> Messages 
        {
            get;
            set; 
        }
    }
}
