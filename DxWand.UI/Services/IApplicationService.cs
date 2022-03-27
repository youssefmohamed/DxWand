using DxWand.UI.Models;

namespace DxWand.UI.Services
{
    public interface IApplicationService
    {
        void SetToken(string token);
        string GetToken();
        bool IsLoggedIn();
        void ClearToken();
        ApplicationUser UserInfo();
    }
}
