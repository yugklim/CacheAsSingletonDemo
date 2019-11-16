using System;

namespace ConsoleApp1.Cache
{
    public sealed class ExampleCache : BaseCache<string>
    {
        private static readonly Lazy<ExampleCache> instance = new Lazy<ExampleCache>(() => new ExampleCache());
        public static ExampleCache Instance => instance.Value;

        private ExampleCache() { }
    }
}
