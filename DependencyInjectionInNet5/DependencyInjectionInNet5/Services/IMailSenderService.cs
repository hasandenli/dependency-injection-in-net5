using System.Collections.Generic;

namespace DependencyInjectionInNet5.Services
{
    public interface IMailSenderService
    {
        bool Send(string from, List<string> toList, string body);
    }
}