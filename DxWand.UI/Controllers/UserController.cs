using System.Collections.Generic;
using System.Threading.Tasks;
using DxWand.UI.Helpers;
using DxWand.UI.Models;
using DxWand.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DxWand.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly string GET_USERS_URL = "/user/manage/getall";
        private readonly string GET_USER_INFO = "/user/manage/getinfo";
        private readonly string GET_USER_MSGS = "/message/get";
        private readonly string POST_USER = "/user/manage/create";
        private readonly IApplicationService _applicationService;
        private readonly IConfiguration _configuration;


        public UserController(IApplicationService applicationService, IConfiguration configuration)
        {
            _applicationService = applicationService;
            _configuration = configuration; 
        }

        public async Task<IActionResult> Index()
        {
            var usersResponse = await HttpClientHelper.GetAsync<ResponseModel<List<ApplicationUser>>>(GET_USERS_URL, _applicationService, _configuration);
            return View(usersResponse);
        }
        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Create([FromForm] CreateUserModel applicationUser)
        {
            if(!ModelState.IsValid)
            {
                return View("Add",applicationUser);
            }

            var createUserResponse = await HttpClientHelper.PostAsync<CreateUserModel, ResponseModel<CreateUserModel>>(POST_USER, applicationUser, _applicationService, _configuration);
            if (!createUserResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, createUserResponse.Message);
                return View("Add", applicationUser);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id) 
        {
            var userInfoResult = await HttpClientHelper.GetAsync<ResponseModel<ApplicationUser>>(GET_USER_INFO + "?id=" + id, _applicationService, _configuration);
            var userMessages = await HttpClientHelper.GetAsync<ResponseModel<List<MessageModel>>>(GET_USER_MSGS + "?id=" + id, _applicationService, _configuration);
            var userDetails = new UserDetailsModel { ApplicationUser = userInfoResult.Data, Messages = userMessages.Data };
            return View("UserDetails",userDetails);
        }
    }
}
