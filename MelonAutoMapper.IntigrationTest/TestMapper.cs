using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MelonAutoMapper.IntigrationTest
{
    [TestClass]
    public class TestMapper
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

        [TestMethod]
        public void TestMap_ShouldWorkWithoutFullNameConfigration()
        {
            Mapper mapper = new Mapper();

            mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, PersonDto>();
            }
            );

            int age = 18;
            string FirstName = "Jack";
            string LastName = "Gao";

            Person p = new Person { Age = age, FirstName=FirstName, LastName=LastName };

            PersonDto pDto = mapper.Map<Person,PersonDto>(p);

            Assert.IsNotNull(pDto);
            Assert.AreEqual(age, pDto.Age);
            Assert.AreEqual(FirstName, pDto.FirstName);
            Assert.AreEqual(LastName, pDto.LastName);
            Assert.AreEqual(null, pDto.FullName);
        }

        [TestMethod]
        public void TestMap_ShouldWorkWithFullNameConfigration()
        {
            Mapper mapper = new Mapper();

            mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, PersonDto>().ForMember(d=>d.FullName,s=>s.FirstName+" "+s.LastName);

            }
            );

            int age = 18;
            string firstName = "Jack";
            string lastName = "Gao";
            string fullName = firstName + " " + lastName;

            Person p = new Person { Age = age, FirstName = firstName, LastName = lastName };

            PersonDto pDto = mapper.Map<Person, PersonDto>(p);

            Assert.IsNotNull(pDto);
            Assert.AreEqual(age, pDto.Age);
            Assert.AreEqual(firstName, pDto.FirstName);
            Assert.AreEqual(lastName, pDto.LastName);
            Assert.AreEqual(fullName, pDto.FullName);
        }
    }
}
