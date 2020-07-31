using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionHelpers.Test
{
    [TestClass]
    public class TestIList
    {
        private IList<int> collection;
        private IList<int> different;

        private string original;

        [TestInitialize]
        public void Initialize()
        {
            this.collection = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25,4,79875148,7451,7146,9,8,976,4797,649,73,31,69,0,9,609,8,50,749,30,409,7454
            };
            this.different = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25,4,79875148,7451,7146,9,8,976,4797,649,73,31,69,0,9,609,8,50,749,30,409,7454
            };

            this.original = ToString(this.different);
        }

        private string ToString(ICollection<int> list)
        {
            return string.Join(", ", list);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.collection = null;
            this.different = null;
            this.original = null;
        }

        [TestMethod]
        public void TestShuffleDefault()
        {
            //act
            collection.ShuffleInplace();
            var result1 = ToString(collection);

            collection.ShuffleInplace();
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleSeed()
        {
            int seed = 666;
            //act
            collection.ShuffleInplace(seed);
            var result1 = ToString(collection);

            different.ShuffleInplace(seed);
            var original2 = ToString(different);

            collection.ShuffleInplace(seed);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreEqual(original2, result1); // shuffling of 2 list which are identical, should result in the same shuffled list
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleRandomWithoutSeed()
        {
            Random random = new Random();
            //act
            collection.ShuffleInplace(random);
            var result1 = ToString(collection);

            different.ShuffleInplace(random);
            var original2 = ToString(different);

            collection.ShuffleInplace(random);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(original2, result1); // shuffling of 2 lists which are identical, should result in the different shuffled lists
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleRandomWithSeed()
        {
            Random random = new Random(666);
            //act
            collection.ShuffleInplace(random);
            var result1 = ToString(collection);

            different.ShuffleInplace(random);
            var original2 = ToString(different);

            collection.ShuffleInplace(random);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(original2, result1); // shuffling of 2 lists which are identical, should result in the different shuffled lists
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }
    }

    [TestClass]
    public class TestIEnumerable
    {
        private IEnumerable<int> collection;
        private IEnumerable<int> different;

        private string original;

        [TestInitialize]
        public void Initialize()
        {
            this.collection = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25,4,79875148,7451,7146,9,8,976,4797,649,73,31,69,0,9,609,8,50,749,30,409,7454
            };
            this.different = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25,4,79875148,7451,7146,9,8,976,4797,649,73,31,69,0,9,609,8,50,749,30,409,7454
            };

            this.original = ToString(this.different);
        }

        private string ToString(IEnumerable<int> list)
        {
            return string.Join(", ", list);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.collection = null;
            this.different = null;
            this.original = null;
        }

        [TestMethod]
        public void TestShuffleDefault()
        {
            //act
            collection = collection.Shuffle();
            var result1 = ToString(collection);

            collection = collection.Shuffle();
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleSeed()
        {
            int seed = 666;
            //act
            collection = collection.Shuffle(seed);
            var result1 = ToString(collection);

            different = different.Shuffle(seed);
            var original2 = ToString(different);

            collection = collection.Shuffle(seed);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreEqual(original2, result1); // shuffling of 2 list which are identical, should result in the same shuffled list
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleRandomWithoutSeed()
        {
            Random random = new Random();
            //act
            collection = collection.Shuffle(random);
            var result1 = ToString(collection);

            different = different.Shuffle(random);
            var original2 = ToString(different);

            collection = collection.Shuffle(random);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(original2, result1); // shuffling of 2 lists which are identical, should result in the different shuffled lists
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleRandomWithSeed()
        {
            Random random = new Random(666);
            //act
            collection = collection.Shuffle(random);
            var result1 = ToString(collection);

            different = different.Shuffle(random);
            var original2 = ToString(different);

            collection = collection.Shuffle(random);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(original2, result1); // shuffling of 2 lists which are identical, should result in the different shuffled lists
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }
    }

    [TestClass]
    public class TestICollections
    {
        private ICollection<int> collection;
        private ICollection<int> different;

        private string original;

        [TestInitialize]
        public void Initialize()
        {
            this.collection = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25,4,79875148,7451,7146,9,8,976,4797,649,73,31,69,0,9,609,8,50,749,30,409,7454
            };
            this.different = new[]
            {
                4,78,4,8,5,98,6,2,7,6,78,25,4,79875148,7451,7146,9,8,976,4797,649,73,31,69,0,9,609,8,50,749,30,409,7454
            };

            this.original = ToString(this.different);
        }

        private string ToString(ICollection<int> list)
        {
            return string.Join(", ", list);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.collection = null;
            this.different = null;
            this.original = null;
        }

        [TestMethod]
        public void TestShuffleDefault()
        {
            //act
            collection = collection.Shuffle();
            var result1 = ToString(collection);

            collection = collection.Shuffle();
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleSeed()
        {
            int seed = 666;
            //act
            collection = collection.Shuffle(seed);
            var result1 = ToString(collection);

            different = different.Shuffle(seed);
            var original2 = ToString(different);

            collection = collection.Shuffle(seed);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreEqual(original2, result1); // shuffling of 2 list which are identical, should result in the same shuffled list
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleRandomWithoutSeed()
        {
            Random random = new Random();
            //act
            collection = collection.Shuffle(random);
            var result1 = ToString(collection);

            different = different.Shuffle(random);
            var original2 = ToString(different);

            collection = collection.Shuffle(random);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(original2, result1); // shuffling of 2 lists which are identical, should result in the different shuffled lists
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }

        [TestMethod]
        public void TestShuffleRandomWithSeed()
        {
            Random random = new Random(666);
            //act
            collection = collection.Shuffle(random);
            var result1 = ToString(collection);

            different = different.Shuffle(random);
            var original2 = ToString(different);

            collection = collection.Shuffle(random);
            var result2 = ToString(collection);

            //assert
            Assert.AreNotEqual(original, result1); // shuffled list should be different from original
            Assert.AreNotEqual(original2, result1); // shuffling of 2 lists which are identical, should result in the different shuffled lists
            Assert.AreNotEqual(result1, result2); // reshuffling should result in a new list
        }
    }
}
