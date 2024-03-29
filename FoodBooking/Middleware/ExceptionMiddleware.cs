﻿namespace FoodBooking.Data.Models.Middleware
{
    using FoodBooking.Data.Models.Exceptions;
    using FoodBooking.Models;
    using System.Net;

    /// <summary>
    /// Exception middleware 
    /// Handles exception message in case of production environment
    /// Logging is not necessary here due the fact that serilog
    /// has its own http request logging middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal server error";

            //Dont forget to add any of exception type to controllers as producing response
            switch (e)
            {
                case TimeoutException _:
                    message = "Request time out";
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    break;

                case BadHttpRequestException _:
                    message = "Bad request";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case MediatorException ex:
                    message = ex.Message;

                    switch (ex.Type)
                    {
                        case ExceptionType.NotFound:
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;

                        case ExceptionType.Error:
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;

                        case ExceptionType.Unauthorized:
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            break;

                        case ExceptionType.NotModified:
                            context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                            break;
                    }
                    break;
                    //and so on...
            }

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
