using System.Collections.Generic;
using System.Threading.Tasks;
using DxWand.UI.Helpers;
using DxWand.UI.Models;
using DxWand.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DxWand.UI.Controllers
{
    public class MessageController : Controller
    {
        private readonly string GET_MESSAGES_URL = "/message/get";
        private readonly string POST_MESSAGES_URL = "/message/create";
        private readonly IApplicationService _applicationService;
        private readonly IConfiguration _configuration;

        public MessageController(IApplicationService applicationService, IConfiguration configuration)
        {
            _applicationService = applicationService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await HttpClientHelper.GetAsync<ResponseModel<List<MessageModel>>>(GET_MESSAGES_URL, _applicationService, _configuration);
            return View(messages);
        }

        public IActionResult Add() 
        {
            return View();
        }

        public async Task<IActionResult> Create([FromForm] AddMessageModel addMessageModel) 
        {
            if (!ModelState.IsValid) 
            {
                return View("Add", addMessageModel);
            }

            var addMessageResult = await HttpClientHelper
                .PostAsync<AddMessageModel, ResponseModel<MessageModel>>(POST_MESSAGES_URL, addMessageModel, _applicationService, _configuration);
            if (!addMessageResult.IsSuccess) 
            {
                ModelState.AddModelError(string.Empty, addMessageResult.Message);
                return View("Add", addMessageResult);
            }

            return RedirectToAction("Index","Message");
        }
    }
}
