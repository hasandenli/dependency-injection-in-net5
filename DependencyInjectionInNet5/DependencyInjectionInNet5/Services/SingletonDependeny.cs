using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5.Services
{
    public class SingletonDependeny
    {
        public static int _counter = 0;
        public SingletonDependeny()
        {
            ++_counter;
        }

        public int GetNextCounter()
        {
            return _counter;
        }
    }
}
