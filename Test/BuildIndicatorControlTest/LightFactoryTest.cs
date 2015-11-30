using BuildIndicatorCommon.ExceptionHandling;
using BuildIndicatorControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace BuildIndicatorControlTest
{
    [TestClass]
    public class LightFactoryTest
    {
        private LightFactory _factoryToTest;

        [TestMethod]
        public void CreateLightForValidDeviceShouldSucceed()
        {
            SetupLightFactory(true);
            var light = _factoryToTest.CreateLight();
            Assert.IsNotNull(light);
        }

        [TestMethod]
        [ExpectedException(typeof(DeviceNotConnectedException))]
        public void CreateLightForInvalidDeviceShouldThrowException()
        {
            SetupLightFactory(false);
            _factoryToTest.CreateLight();
        }

        private void SetupLightFactory(bool deviceConnected)
        {
            var mockDeviceController = new Mock<IDeviceController>(MockBehavior.Strict);
            mockDeviceController.Setup(x => x.OpenDevice()).Returns(deviceConnected);
            
            _factoryToTest = new LightFactory(mockDeviceController.Object);
        }
    }
}
