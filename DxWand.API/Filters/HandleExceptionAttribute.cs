using System;
using DxWand.Application.Responses;
using DxWand.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DxWand.API.Filters
{
    public class HandleExceptionAttribute : Attribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            var responseMessage = new ResponseMessage<string>
            {
                IsSuccess = false,
                Data = null,
                Message = context.Exception.Message,
                StatusCode = Convert.ToInt32(StatusCodeEnum.InternalServerError)
            };

            context.Result = new ObjectResult(responseMessage)
            {
                StatusCode = responseMessage.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
