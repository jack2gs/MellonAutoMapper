using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace MelonAutoMapper.UnitTest
{
    [TestClass]
    public class TestTypeMapExpression
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

        TypeMapExpression<Person, PersonDto> _entry;

        [TestInitialize]
        public void Tearup()
        {
            _entry = new TypeMapExpression<Person, PersonDto>();
        }

        [TestMethod]
        public void TestNew_TheItemCountShouldBeEqualToTheCountOfDestinationProperty()
        {
            // act
            int expected = typeof(PersonDto).GetProperties().Length - 1;

            //assert
            Assert.AreEqual(expected, _entry.ItemCount);
        }

        [TestMethod]
        public void TestGetFunc_FuncForAge_ShouldNotBeNULL()
        {
            // arrange
            PropertyInfo pi = typeof(PersonDto).GetProperty("Age");

            // act
            LambdaExpression expression = _entry.GetFunction(pi);

            //assert
            Assert.IsNotNull(expression);
        }

        [TestMethod]
        public void TestGetFunc_FuncForFullName_ShouldBeNULL()
        {
            // arrange
            PropertyInfo pi = typeof(PersonDto).GetProperty("FullName");

            // act
            LambdaExpression expression = _entry.GetFunction(pi);

            //assert
            Assert.IsNull(expression);
        }

        [TestMethod]
        public void TestForMember_FuncForFullName_ShouldNotBeNULL()
        {
            // arrange
            PropertyInfo pi = typeof(PersonDto).GetProperty("FullName");

            // act
            Expression<Func<Person, object>> expected = s => s.FirstName + " " + s.LastName;
            _entry.ForMember(d=>d.FullName, expected);
            LambdaExpression actual = _entry.GetFunction(pi);

            //assert
            Assert.AreEqual(expected, actual);
        }

        public void TestForMember_CanChained()
        {
            try
            {
                // act
                _entry.ForMember(d => d.FullName, s => s.FirstName + " " + s.LastName).ForMember(d => d.Age, s => s.Age);
            }
            catch (Exception)
            {
                //assert
                Assert.IsFalse(true);
            }
        }
    }
}
