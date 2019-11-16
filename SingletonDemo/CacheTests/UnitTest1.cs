using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Func<IEnumerable<string>, string> valueFactory = (keyParts) => string.Join("_", keyParts);
        private readonly Func<string, string> valueFactory2 = (key) => key;

        [TestMethod]
        public void Tests_If_Value_Added_For_Key_As_Sequence_Of_Strings()
        {
            // Assign
            var p1p2Value = ExampleCache.Instance.GetOrAdd(new[] {"p1", "p2"}, valueFactory);

            // Assert
            Assert.AreEqual("p1_p2", p1p2Value);
        }

        [TestMethod]
        public void Tests_If_Value_Added_For_Key_As_String()
        {
            // Assign
            var p1Value = ExampleCache.Instance.GetOrAdd("key", valueFactory2);

            // Assert
            Assert.AreEqual("key", p1Value);
        }

        [TestMethod]
        public void Tests_If_Everything_Is_Cleared_In_Cache()
        {
            // Assign
            ExampleCache.Instance.GetOrAdd(new[] { "p1", "p2" }, valueFactory);
            ExampleCache.Instance.GetOrAdd("key", valueFactory2);

            //Act
            ExampleCache.Instance.ClearCache("all");

            // Asserts no exceptions
        }

        [TestMethod]
        public void Tests_If_Parts_Are_Cleared_In_Cache()
        {
            // Assign
            ExampleCache.Instance.GetOrAdd(new[] { "p1", "p2" }, valueFactory);
            ExampleCache.Instance.GetOrAdd("key", valueFactory2);

            //Act
            ExampleCache.Instance.ClearCache("p");

            // Asserts no exceptions
        }
    }
}
