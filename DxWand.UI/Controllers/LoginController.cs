using System.Threading.Tasks;
using DxWand.UI.Helpers;
using DxWand.UI.Models;
using DxWand.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DxWand.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IConfiguration _configuration;
        private readonly string LOGIN_URL = "/user/access/login";

        public LoginController( IApplicationService applicationService, IConfiguration configuration)
        {
            _applicationService = applicationService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            if(_applicationService.IsLoggedIn())
                return RedirectToAction("Index","Home");
                
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            if (!ModelState.IsValid) 
            {
                return View("Index", loginModel);
            }


            var response = await HttpClientHelper.PostAsync<LoginModel, ResponseModel<string>>(LOGIN_URL, loginModel, _applicationService, _configuration);
            if(response == null || !response.IsSuccess) 
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View("Index", loginModel);
            }

            _applicationService.SetToken(response.Data);

            return RedirectToAction("Index","Home");
        }

        public IActionResult Logout() 
        {
            _applicationService.ClearToken();
            return RedirectToAction("Index","Login");
        }
    }
}
