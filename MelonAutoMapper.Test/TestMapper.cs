using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MelonAutoMapper.UnitTest
{
    [TestClass]
    public class TestMapper
    {
        private Mapper _mapper;

        [TestInitialize]
        public void TearUp()
        {
            _mapper = new Mapper();
        }

        [TestCleanup]
        public void TearDown()
        {
            _mapper = null;
        } 

        [TestMethod]
        public void TestInitialize_CanBeCalled()
        {
            try
            {
                _mapper.Initialize(cfg =>
                {
                });
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestConfig_WhenNotInitialized_IsNull()
        {
            Assert.IsNull(_mapper.Configration);
        }

        [TestMethod]
        public void TestConfig_WhenInitialized_NotNull()
        {
            _mapper.Initialize(cfg => { });
            Assert.IsNotNull(_mapper.Configration);
        }

        [TestMethod]
        public void TestInitialize_LambdaShouldBeCalled()
        {
            // arrange
            bool isCalled = false;
            Action<MapperConfigration> action = cfg =>
            {
                isCalled = true; 
            };

            // act
            _mapper.Initialize(action);

            // assert
            Assert.IsTrue(isCalled);

        }

        [TestMethod]
        public void TestInitialize_ConfigrationShouldNotBeNull()
        {
            _mapper.Initialize(cfg =>
            {
                Assert.IsNotNull(cfg);
            });
        }
    }
}
