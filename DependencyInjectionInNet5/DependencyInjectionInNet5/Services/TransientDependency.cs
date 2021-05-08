using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5.Services
{
    public class TransientDependency
    {
        public static int _counter = 0;
        public TransientDependency()
        {
            ++_counter;
        }

        public int GetNextCounter()
        {
            return _counter;
        }
    }
}
