using DependencyInjectionInNet5.Configuration;
using DependencyInjectionInNet5.Models;
using DependencyInjectionInNet5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Linq;

namespace DependencyInjectionInNet5.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailSenderService _mailSenderService;
        private readonly MailSettingsConfiguration _mailConfiguration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IMailSenderService mailSenderService,
            IOptions<MailSettingsConfiguration> mailConfiguration
            )
        {
            _mailSenderService = mailSenderService;
            _mailConfiguration = mailConfiguration.Value;
        }

        //public HomeController(
        //    IMailSenderService mailSenderService,
        //    IOptions<MailSettingsConfiguration> mailConfiguration,
        //    ILogger<HomeController> logger
        //    )
        //{
        //    _mailSenderService = mailSenderService;
        //    _mailConfiguration = mailConfiguration.Value;
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMail()
        {
            //POCO (Plain Old CLR Objects)
            SendMailViewModel viewModel = new SendMailViewModel();

            viewModel.Status = _mailSenderService.Send(
                _mailConfiguration.From,
                _mailConfiguration.To.Split(';').OfType<string>().ToList(),
                "Dummy Mail");

            if(!viewModel.Status)
                viewModel.Message = "Mail could not be sent!";
            else
                viewModel.Message = "Mail has been sent.";

            return View(viewModel);
        }

        /// <summary>
        /// Version Of Action Injection
        /// </summary>
        /// <returns></returns>
        public IActionResult SendMailActionInjection([FromServices] IMailSenderService mailSenderService)
        {
            //POCO (Plain Old CLR Objects)
            SendMailViewModel viewModel = new SendMailViewModel();

            viewModel.Status = mailSenderService.Send(
                _mailConfiguration.From,
                _mailConfiguration.To.Split(';').OfType<string>().ToList(),
                "Dummy Mail");

            if (!viewModel.Status)
                viewModel.Message = "Mail could not be sent!";
            else
                viewModel.Message = "Mail has been sent.";

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
