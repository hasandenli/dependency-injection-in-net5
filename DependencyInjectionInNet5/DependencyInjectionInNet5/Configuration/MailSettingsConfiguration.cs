using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5.Configuration
{
    public class MailSettingsConfiguration
    {
        public string From { get; set; }
        public string To { get; set; }
        public bool Avaiable { get; set; }
    }
}
