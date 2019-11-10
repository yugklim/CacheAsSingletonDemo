using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache
{
    public sealed class ExampleCache : BaseCache<bool?>
    {
        private static readonly Lazy<ExampleCache> instance = new Lazy<ExampleCache>(() => new ExampleCache());
        public static ExampleCache Instance => instance.Value;

        private ExampleCache() { }
    }
}
