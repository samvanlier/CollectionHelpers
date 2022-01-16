using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionHelpers.Test
{
    [TestClass]
    public class TestMinMax
    {
        private IEnumerable<int> collection;

        [TestInitialize]
        public void Initialize()
        {
            this.collection = new[]
            {
                3, 4, 5, 7, 0, -1, 9, 0, 9
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.collection = null;
        }

        [TestMethod]
        public void TestMaxFound()
        {
            int index = 6;
            int res = collection.MaxIndex();

            Assert.AreEqual(index, res);
        }

        [TestMethod]
        public void TestMaxEmptyList()
        {
            int index = -1;
            int res = new int[] { }.MaxIndex();

            Assert.AreEqual(index, res);
        }

        [TestMethod]
        public void TestMinFound()
        {
            int index = 5;
            int res = collection.MinIndex();

            Assert.AreEqual(index, res);
        }

        [TestMethod]
        public void TestMinEmptyList()
        {
            int index = -1;
            int res = new int[] { }.MinIndex();

            Assert.AreEqual(index, res);
        }
    }
}
