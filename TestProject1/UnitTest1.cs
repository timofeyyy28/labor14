using ClassLibraryLabor10;
using lab14;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class CollectionTests
    {
        private SortedDictionary<string, List<Musicalinstrument>> concert;

        public CollectionTests()
        {
            concert = new SortedDictionary<string, List<Musicalinstrument>>
            {
                { "Band1", new List<Musicalinstrument>
                    {
                        new Guitar { Name = "guitar1" },
                        new ElectricGuitar { Name = "electricGuitar1", GuitarPowerSupply = "Аккумулятор" },
                        new Piano { Name = "piano1" }
                    }
                },
                { "Band2", new List<Musicalinstrument>
                    {
                        new Guitar { Name = "guitar2" },
                        new ElectricGuitar { Name = "electricGuitar2", GuitarPowerSupply = "Батарейка" }
                    }
                }
            };
        }

        [TestMethod]
        public void GetNamesAndTypesOfGuitar_Test()
        {
            var expected = new List<(string Name, string Type)>
            {
                ("GUITAR1", "Guitar"),
                ("GUITAR2", "Guitar")
            };

            var result = Collection.GetNamesAndTypesOfGuitar(concert);

            CollectionAssert.AreEquivalent(expected, result.ToList());
        }

        [TestMethod]
        public void GetNamesAndTypesOfGuitarExt_Test()
        {
            var expected = new List<(string Name, string Type)>
            {
                ("GUITAR1", "Guitar"),
                ("GUITAR2", "Guitar")
            };

            var result = Collection.GetNamesAndTypesOfGuitarExt(concert);

            CollectionAssert.AreEquivalent(expected, result.ToList());
        }

        [TestMethod]
        public void GetAveragePower_Test()
        {
            var expected = 0.0; // No numeric power supply values to average, set accordingly

            var result = Collection.GetAveragePower(concert);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAveragePowerExt_Test()
        {
            var expected = 0.0; // No numeric power supply values to average, set accordingly

            var result = Collection.GetAveragePowerExt(concert);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GroupByType_Test()
        {
            var result = Collection.GroupByType(concert);

            var expected = new List<IGrouping<string, Musicalinstrument>>
            {
                new TestGrouping<string, Musicalinstrument>("Guitar", new List<Musicalinstrument>
                {
                    new Guitar { Name = "guitar1" },
                    new Guitar { Name = "guitar2" }
                }),
                new TestGrouping<string, Musicalinstrument>("ElectricGuitar", new List<Musicalinstrument>
                {
                    new ElectricGuitar { Name = "electricGuitar1", GuitarPowerSupply = "Аккумулятор" },
                    new ElectricGuitar { Name = "electricGuitar2", GuitarPowerSupply = "Батарейка" }
                }),
                new TestGrouping<string, Musicalinstrument>("Piano", new List<Musicalinstrument>
                {
                    new Piano { Name = "piano1" }
                })
            };

            CollectionAssert.AreEquivalent(expected.ToList(), result.ToList());
        }

        [TestMethod]
        public void GroupByTypeExt_Test()
        {
            var result = Collection.GroupByTypeExt(concert);

            var expected = new List<IGrouping<string, Musicalinstrument>>
            {
                new TestGrouping<string, Musicalinstrument>("Guitar", new List<Musicalinstrument>
                {
                    new Guitar { Name = "guitar1" },
                    new Guitar { Name = "guitar2" }
                }),
                new TestGrouping<string, Musicalinstrument>("ElectricGuitar", new List<Musicalinstrument>
                {
                    new ElectricGuitar { Name = "electricGuitar1", GuitarPowerSupply = "Аккумулятор" },
                    new ElectricGuitar { Name = "electricGuitar2", GuitarPowerSupply = "Батарейка" }
                }),
                new TestGrouping<string, Musicalinstrument>("Piano", new List<Musicalinstrument>
                {
                    new Piano { Name = "piano1" }
                })
            };

            CollectionAssert.AreEquivalent(expected.ToList(), result.ToList());
        }

        [TestMethod]
        public void MergeCollections_Test()
        {
            var concert2 = new SortedDictionary<string, List<Musicalinstrument>>
            {
                { "Band3", new List<Musicalinstrument>
                    {
                        new Guitar { Name = "guitar1" },
                        new Piano { Name = "piano2" }
                    }
                }
            };

            var result = Collection.MergeCollections(concert, concert2);

            var expected = new List<IGrouping<string, Musicalinstrument>>
            {
                new TestGrouping<string, Musicalinstrument>("guitar1", new List<Musicalinstrument>
                {
                    new Guitar { Name = "guitar1" },
                    new Guitar { Name = "guitar1" }
                })
            };

            CollectionAssert.AreEquivalent(expected.ToList(), result.ToList());
        }

        [TestMethod]
        public void MergeCollectionsExt_Test()
        {
            var concert2 = new SortedDictionary<string, List<Musicalinstrument>>
            {
                { "Band3", new List<Musicalinstrument>
                    {
                        new Guitar { Name = "guitar1" },
                        new Piano { Name = "piano2" }
                    }
                }
            };

            var result = Collection.MergeCollectionsExt(concert, concert2);

            var expected = new List<IGrouping<string, Musicalinstrument>>
            {
                new TestGrouping<string, Musicalinstrument>("guitar1", new List<Musicalinstrument>
                {
                    new Guitar { Name = "guitar1" },
                    new Guitar { Name = "guitar1" }
                })
            };

            CollectionAssert.AreEquivalent(expected.ToList(), result.ToList());
        }

        [TestMethod]
        public void MergeWithBands_Test()
        {
            var bands = new List<Band>
            {
                new Band("guitar1") { Name = "Band1" },
                new Band("electricGuitar2") { Name = "Band2" },
                new Band("piano1") { Name = "Band3" }
            };

            var result = Collection.MergeWithBands(concert, bands);

            Assert.AreEqual(3, result.Count()); // 3 matching instruments
        }

        [TestMethod]
        public void MergeWithBandsExt_Test()
        {
            var bands = new List<Band>
            {
                new Band("guitar1") { Name = "Band1" },
                new Band("electricGuitar2") { Name = "Band2" },
                new Band("piano1") { Name = "Band3" }
            };

            var result = Collection.MergeWithBandsExt(concert, bands);

            Assert.AreEqual(3, result.Count()); // 3 matching instruments
        }
    }

    public class TestGrouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        private readonly IEnumerable<TElement> _elements;

        public TestGrouping(TKey key, IEnumerable<TElement> elements)
        {
            Key = key;
            _elements = elements;
        }

        public TKey Key { get; }

        public IEnumerator<TElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
