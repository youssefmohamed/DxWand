using System.Threading.Tasks;
using DxWand.UI.Helpers;
using DxWand.UI.Models;
using DxWand.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IConfiguration _configuration;
        private readonly string DASHBOARD_URL = "/dashboard/statistics";

        public HomeController(IApplicationService applicationService, IConfiguration configuration)
        {
            _applicationService = applicationService;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            if (!_applicationService.IsLoggedIn())
                return RedirectToAction("Index", "Login");
            if (!_applicationService.UserInfo().IsAdmin)
                return RedirectToAction("Index", "Message");

            var dashboardResponse = await HttpClientHelper.GetAsync<ResponseModel<DashboardModel>>(DASHBOARD_URL,_applicationService,_configuration);
            return View(dashboardResponse);
        }

    }
}
