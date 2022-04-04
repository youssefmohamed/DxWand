using System;
using DxWand.Application.Responses;
using DxWand.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DxWand.API.Filters
{
    public class HandleExceptionAttribute : Attribute, IExceptionFilter
    {
        private readonly ILogger<HandleExceptionAttribute> _logger;
        public HandleExceptionAttribute(ILogger<HandleExceptionAttribute> logger)
        {
            _logger = logger;
        }
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            var responseMessage = new ResponseMessage<string>
            {
                IsSuccess = false,
                Data = null,
                Message = context.Exception.Message,
                StatusCode = Convert.ToInt32(StatusCodeEnum.InternalServerError)
            };

            _logger.LogError(context.Exception.Message);

            context.Result = new ObjectResult(responseMessage)
            {
                StatusCode = responseMessage.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
