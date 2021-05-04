using DependencyInjectionInNet5.Controllers;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using DependencyInjectionInNet5.Models;
using System.Diagnostics.CodeAnalysis;
using DependencyInjectionInNet5.Services;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DependencyInjectionInNet5.Configuration;

namespace DependencyInjectionInNet5.Test
{
    [ExcludeFromCodeCoverage]
    public class HomeControllerTest
    {
        private readonly IServiceProvider _services = Program.CreateHostBuilder(new string[] { }).Build().Services;

        HomeController _homeController;

        public HomeControllerTest()
        {
           
        }

        [Fact]
        public void SendMail_ReturnExpectedViewModel_WhenSuccess()
        {
            var mockService = new Mock<IMailSenderService>();
            var mockConfigurationService = new Mock<IOptions<MailSettingsConfiguration>>();

            mockService.Setup(m => m.Send("x@hotmail.com",
                                          new List<string>() { "y@hotmail.com", "z@hotmail.com" },
                                          "Dummy Mail")).Returns(true);

            mockConfigurationService.Setup(m => m.Value).Returns(new MailSettingsConfiguration()
            {
                From = "x@hotmail.com",
                To = "y@hotmail.com;z@hotmail.com"
            });

            _homeController = new HomeController(mockService.Object, mockConfigurationService.Object);
            var result = _homeController.SendMail();

            var viewModel = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<SendMailViewModel>(viewModel.ViewData.Model);

            Assert.True(model.Status);
        }

        [Fact]
        public void SendMail_ReturnExpectedViewModel_WhenFailed()
        {
            var mockService = new Mock<IMailSenderService>();
            var mockConfigurationService = new Mock<IOptions<MailSettingsConfiguration>>();

            mockService.Setup(m => m.Send("x@hotmail.com",
                                          new List<string>() { "y@hotmail.com", "z@hotmail.com" },
                                          "Dummy Mail")).Returns(false);

            mockConfigurationService.Setup(m => m.Value).Returns(new MailSettingsConfiguration()
            {
                From = "x@hotmail.com",
                To = "y@hotmail.com;z@hotmail.com"
            });

            _homeController = new HomeController(mockService.Object, mockConfigurationService.Object);
            var result = _homeController.SendMail();

            var viewModel = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<SendMailViewModel>(viewModel.ViewData.Model);

            Assert.False(model.Status);
        }
    }
}
