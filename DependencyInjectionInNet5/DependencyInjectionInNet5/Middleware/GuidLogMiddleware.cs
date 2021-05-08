using DependencyInjectionInNet5.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5.Middleware
{
    public class GuidLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IGuidService _guidService;

        public GuidLogMiddleware(
            RequestDelegate next, 
            ILogger<GuidLogMiddleware> logger
            )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, IGuidService guidService)
        {
            if(httpContext.Request.Path == "/Guid/Index")
            {
                string logMessage = $"Middleware : The GUID from Guid : {guidService.GetGuid()}";

                _logger.LogInformation(logMessage);
            }
            
            await _next(httpContext); // calling next middleware
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GuidLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseGuidLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GuidLogMiddleware>();
        }
    }
}
