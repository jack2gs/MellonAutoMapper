using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MelonAutoMapper.UnitTest
{
    [TestClass]
    public class TestMapperConfigration
    {
        class Person
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public int Age { get; set; }
        }

        class PersonDto
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public int Age { get; set; }

            public string FullName { get; set; }
        }

        private MapperConfigration _config;

        [TestInitialize]
        public void TearUp()
        {
            _config = new MapperConfigration();
        }

        [TestCleanup]
        public void TearDown()
        {
            _config = null;
        }

        [TestMethod]
        public void TestEntryCount_ShouldBeZero()
        {
            Assert.AreEqual(0, _config.EntryCount);
        }

        [TestMethod]
        public void TestCreateMap_CanSetupConfigration()
        {
            // arrange
            MapperConfigration config = new MapperConfigration();

            // act
            config.CreateMap<PersonDto, Person>();

            // assert
            Assert.AreEqual(1, config.EntryCount);
        }

        [TestMethod]
        public void TestCreateMap_EntryCountShouldBeTwo()
        {
            // arrange
            MapperConfigration config = new MapperConfigration();

            // act
            config.CreateMap<PersonDto, Person>();
            config.CreateMap<Person, PersonDto>();

            // assert
            Assert.AreEqual(2, config.EntryCount);
        }

        [TestMethod]
        public void TestCreateMap_ConfigEntryShouldNotBeNull()
        {
            // act
            var entry = _config.CreateMap<PersonDto, Person>();

            // assert
            Assert.IsNotNull(entry);
        }

        [TestMethod]
        public void TestGetTypeMapConfigration_ConfigrationShouldBeNull()
        {
            var actual = _config.GetTypeMapConfigration<Person, PersonDto>();

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void TestGetTypeMapConfigration_ConfigrationShouldNotBeNull()
        {
            _config.CreateMap<PersonDto, Person>();

            var actual = _config.GetTypeMapConfigration<PersonDto, Person>();

            Assert.IsNotNull(actual);
        }
    }
}
