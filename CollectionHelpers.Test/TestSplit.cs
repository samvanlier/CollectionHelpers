using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionHelpers.Test
{
    [TestClass]
    public class TestSplit
    {
        private IEnumerable<int> enumerable;
        private ICollection<int> collection;

        [TestInitialize]
        public void Initialize()
        {
            this.enumerable = new[]
            {
                4,8,4,8,5,9,6,2,7,6, //chunk 0
                7,2,4,8,1,1,6,9,8,9, //chunk 1
                6,7,3,7,0,9,6,8,5,4, //chunk 2
                3,8,4                //chunk 3
            };
            this.collection = this.enumerable.ToList();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.enumerable = null;
            this.collection = null;
        }

        [TestMethod]
        public void TestSuccesEnumerable()
        {
            //act
            var chunks = enumerable.Split(10);

            //assert
            Assert.AreEqual(4, chunks.Count());
            Assert.AreEqual(10, chunks.ElementAt(0).Count());
            Assert.AreEqual(10, chunks.ElementAt(1).Count());
            Assert.AreEqual(10, chunks.ElementAt(2).Count());
            Assert.AreEqual(3, chunks.ElementAt(3).Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "groupSize is not allowed to be 0")]
        public void TestExceptionZeroEnumerable()
        {
            var chunks = enumerable.Split(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "groupSize is not allowed to be negative")]
        public void TestExceptionNegativeEnumerable()
        {
            var chunks = enumerable.Split(-10);
        }

        [TestMethod]
        public void TestSuccesCollection()
        {
            //act
            var chunks = collection.Split(10);

            //assert
            Assert.AreEqual(4, chunks.Count);
            Assert.AreEqual(10, chunks.ElementAt(0).Count);
            Assert.AreEqual(10, chunks.ElementAt(1).Count);
            Assert.AreEqual(10, chunks.ElementAt(2).Count);
            Assert.AreEqual(3, chunks.ElementAt(3).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "groupSize is not allowed to be 0")]
        public void TestExceptionZeroCollection()
        {
            var chunks = enumerable.Split(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "groupSize is not allowed to be negative")]
        public void TestExceptionNegativeCollection()
        {
            var chunks = enumerable.Split(-10);
        }
    }
}
