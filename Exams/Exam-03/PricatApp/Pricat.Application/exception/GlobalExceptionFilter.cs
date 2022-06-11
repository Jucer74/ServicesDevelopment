using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocialMedia.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BadRequestException))
            {
                var exception = (BadRequestException)context.Exception;
                var validation = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = exception.Message
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
            
            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                var exception = (NotFoundException)context.Exception;
                var validation = new
                {
                    Status = 200,
                    Title = "OK",
                    Detail = exception.Message
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new OkObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(InternalServerErrorException))
            {
                var exception = (InternalServerErrorException)context.Exception;
                var validation = new
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = exception.Message
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }

            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                var validation = new
                {
                    Status = 404,
                    Title = "Nort Found",
                    Detail = exception.Message
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.ExceptionHandled = true;
            }


        }
    }
}