using DependencyInjectionInNet5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5.Controllers
{
    public class GuidController : Controller
    {
        private readonly IGuidService _guidService;
        private readonly ILogger<HomeController> _logger;

        public GuidController(
            IGuidService guidService,
            ILogger<HomeController> logger
            )
        {
            _guidService = guidService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string logMessage = $"GuidController 1: The GUID from Guid : {_guidService.GetGuid()}";

            _logger.LogInformation(logMessage);

            logMessage = $"GuidController 2: The GUID from Guid : {_guidService.GetGuid()}";

            _logger.LogInformation(logMessage);

            return Ok();
        }
    }
}
