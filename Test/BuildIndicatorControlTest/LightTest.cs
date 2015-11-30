using System;
using BuildIndicatorCommon.ExceptionHandling;
using BuildIndicatorControl;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildIndicatorControlTest
{
    [TestClass]
    public class LightTest
    {
        private int _turnOffCount;
        private int _turnOnCount;
        private int _flashTurnOffCount;
        private int _flashTurnOnCount;
        private bool _lightChangedToRed;
        private bool _lightChangedToGreen;
        private bool _lightChangedToYellow;

        private readonly int _numberOfLightColors = Enum.GetNames(typeof(Light.Color)).Length;
        
        [TestMethod]
        public void TurnOffAllLightsShouldTurnOffAllColors()
        {
            var lightToTest = SetupMockDeviceController();
            lightToTest.TurnOff(true);

            Assert.AreEqual(_numberOfLightColors, _turnOffCount);
            Assert.IsTrue(_lightChangedToRed);
            Assert.IsTrue(_lightChangedToYellow);
            Assert.IsTrue(_lightChangedToGreen);
        }

        [TestMethod]
        public void TurnOffLightShouldOnlyTurnOffCurrentColor()
        {
            var lightToTest = SetupMockDeviceController();
            lightToTest.LightColor = Light.Color.Yellow;

            lightToTest.TurnOff();

            Assert.AreEqual(1, _turnOffCount);
            Assert.IsFalse(_lightChangedToGreen);
            Assert.IsFalse(_lightChangedToRed);
        }

        [TestMethod]
        public void TurnOnLight()
        {
            var lightToTest = SetupMockDeviceController();

            lightToTest.TurnOn();

            Assert.AreEqual(1, _turnOnCount);
            Assert.AreEqual(0, _flashTurnOnCount);
        }

        [TestMethod]
        public void TurnOnFlash()
        {
            var lightToTest = SetupMockDeviceController();
            lightToTest.TurnOn(true);

            Assert.AreEqual(1, _turnOnCount);
            Assert.AreEqual(1, _flashTurnOnCount);
        }

        [TestMethod]
        [ExpectedException(typeof(DeviceNotConnectedException))]
        public void TurnOnWithoutDeviceShouldThrowException()
        {
            var lightToTest = SetupMockDeviceController(false);
            lightToTest.TurnOn();
        }

        private Light SetupMockDeviceController(bool isDeviceOpen = true)
        {
            var mockDeviceController = new Mock<IDeviceController>(MockBehavior.Strict);

            //Setup Turn Off Device
            mockDeviceController.Setup(p => p.TurnOffDevice()).Callback(() => _turnOffCount++);
            mockDeviceController.Setup(p => p.TurnOffFlash()).Callback(() => _flashTurnOffCount++);
            mockDeviceController.Setup(p => p.SetDeviceColor(Light.Color.Red)).Callback(() => _lightChangedToRed = true);
            mockDeviceController.Setup(p => p.SetDeviceColor(Light.Color.Yellow)).Callback(() => _lightChangedToYellow = true);
            mockDeviceController.Setup(p => p.SetDeviceColor(Light.Color.Green)).Callback(() => _lightChangedToGreen = true);

            //Setup Turn On Device
            mockDeviceController.Setup(p => p.TurnOnDevice()).Callback(() => _turnOnCount++);
            mockDeviceController.Setup(p => p.TurnOnFlash()).Callback(() => _flashTurnOnCount++);

            //Setup IsDeviceOpen
            mockDeviceController.Setup(p => p.IsDeviceOpen()).Returns(isDeviceOpen);

            return new Light(mockDeviceController.Object);
        }
    }
}
