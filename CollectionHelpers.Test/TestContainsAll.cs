using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHelpers.Test
{
    [TestClass]
    public class TestContainsAll
    {
        private IEnumerable<int> collection;
        private IEnumerable<int> subset;

        [TestInitialize]
        public void Initialize()
        {
            this.collection = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25,4,79875148,7451,7146,9,8,976,4797,649,73,31,69,0,9,609,8,50,749,30,409,7454
            };
            this.subset = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.collection = null;
            this.subset = null;
        }

        [TestMethod]
        public void TestContainsAllTrue()
        {
            bool result = collection.ContainsAll(subset);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestContainsAllFalse()
        {
            bool result = collection.ContainsAll(new[] { 666, 777, 0, 4, 8, 4545218 });

            Assert.IsFalse(result);
        }
    }
}
