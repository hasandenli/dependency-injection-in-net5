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
        private readonly TransientDependency _transientDependency;

        public GuidService(ScopedDependency scopedDependency)
        {
            _guid = Guid.NewGuid();
            _scopedDependency = scopedDependency;
        }

        public GuidService(ScopedDependency scopedDependency, TransientDependency transientDependency)
        {
            _guid = Guid.NewGuid();
            _scopedDependency = scopedDependency;
            _transientDependency = transientDependency;
        }

        public string GetGuid() => _guid.ToString();
    }
}
