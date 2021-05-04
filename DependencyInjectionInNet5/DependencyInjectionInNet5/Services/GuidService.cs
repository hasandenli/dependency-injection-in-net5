using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5.Services
{
    public class GuidService : IGuidService
    {
        private readonly Guid _guid;
        private readonly ScopedDependency _scopedDependency;

        public GuidService(ScopedDependency scopedDependency)
        {
            _guid = Guid.NewGuid();
            _scopedDependency = scopedDependency;
        }

        public string GetGuid() => _guid.ToString();
    }
}
