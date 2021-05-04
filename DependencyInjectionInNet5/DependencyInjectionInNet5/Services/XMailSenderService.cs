using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5.Services
{
    public class XMailSenderService : IMailSenderService
    {
        public bool Send(string from, List<string> toList, string body)
        {
            // Implementation Third-Party Service With X Company

            return true;
        }
    }
}
