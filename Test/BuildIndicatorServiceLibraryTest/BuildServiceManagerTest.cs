using System.Threading;
using System.Threading.Tasks;
using BuildIndicatorCommon.Model;
using BuildIndicatorControl;
using BuildIndicatorServiceLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildIndicatorServiceLibraryTest
{
    [TestClass]
    public class BuildServiceManagerTest
    {
        private const int WaitTime = 1500;

        private Task _threadTask;

        private int _turnOffCount;
        private int _turnOnCount;
        private int _flashTurnOffCount;
        private int _flashTurnOnCount;

        private const int TestTimeout = 20000;

// ReSharper disable NotAccessedField.Local
        private bool _lightColorChanged;
// ReSharper restore NotAccessedField.Local

        [TestCleanup]
        public void Cleanup()
        {
            _turnOffCount = 0;
            _turnOnCount = 0;
            _flashTurnOffCount = 0;
            _flashTurnOnCount = 0;
            _lightColorChanged = false;
        }

        [TestMethod, Timeout(TestTimeout)]
        public void RunBuildServiceManagerWithSuccessfulBuild()
        {
            var buildToSimulate = new Build {BuildNumber = "1", IsBuilding = true, State = Build.BuildState.Success};
            var buildServiceManagerToTest = SetupMockData(buildToSimulate);

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);

            Thread.Sleep(WaitTime);

            buildServiceManagerToTest.Stop();

            _threadTask.Wait();

            Assert.AreEqual(Light.Color.Green, buildServiceManagerToTest.Light.LightColor);
            Assert.AreNotEqual(0, _flashTurnOnCount);
        }

        [TestMethod, Timeout(TestTimeout)]
        public void RunBuildServiceManagerWithUnstableBuild()
        {
            var buildToSimulate = new Build { BuildNumber = "1", IsBuilding = false, State = Build.BuildState.Unstable };
            var buildServiceManagerToTest = SetupMockData(buildToSimulate);

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);

            Thread.Sleep(WaitTime);

            buildServiceManagerToTest.Stop();

            _threadTask.Wait();

            //verify that the light has been set to yellow
            Assert.AreEqual(Light.Color.Yellow, buildServiceManagerToTest.Light.LightColor);
            Assert.AreEqual(0, _flashTurnOnCount);
        }

        [TestMethod, Timeout(TestTimeout)]
        public void ResetBuildServiceManagerShouldTurnOffAllLights()
        {
            var buildToSimulate = new Build { BuildNumber = "1", IsBuilding = false, State = Build.BuildState.Failure };
            var buildServiceManagerToTest = SetupMockData(buildToSimulate);

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);

            Thread.Sleep(WaitTime);

            //pause so we can reset the turn off count
            buildServiceManagerToTest.Pause();

            Thread.Sleep(WaitTime);

            _turnOffCount = 0;

            buildServiceManagerToTest.Reset();

            //verify that the all light colors have been turned off
            Assert.AreEqual(3, _turnOffCount);

            buildServiceManagerToTest.Stop();

            _threadTask.Wait();
        }

        [TestMethod, Timeout(TestTimeout)]
        public void ChangingSourceInBuildServiceManagerShouldShowNewSourceStatus()
        {
            var buildToSimulate = new Build {IsBuilding = false, State = Build.BuildState.Unknown};
            var newSourceBuildState = new Build {IsBuilding = true, State = Build.BuildState.Failure};

            var buildServiceManagerToTest = SetupMockData(buildToSimulate);

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);

            buildServiceManagerToTest.BuildSource = new User {Build = newSourceBuildState};

            Thread.Sleep(WaitTime);

            buildServiceManagerToTest.Stop();

            _threadTask.Wait();

            Assert.AreEqual(Light.Color.Red, buildServiceManagerToTest.Light.LightColor);
            Assert.AreNotEqual(0, _flashTurnOnCount);
        }

        [TestMethod, Timeout(TestTimeout)]
        public void PausingServiceManagerShouldShowPausedState()
        {
            var buildToSimulate = new Build { BuildNumber = "1", IsBuilding = false, State = Build.BuildState.Failure };
            var buildServiceManagerToTest = SetupMockData(buildToSimulate);

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);

            buildServiceManagerToTest.Pause();

            Assert.AreEqual(BuildServiceManager.State.Paused, buildServiceManagerToTest.CurrentState);

            buildServiceManagerToTest.Stop();

            _threadTask.Wait();
        }

        [TestMethod, Timeout(TestTimeout)]
        public void StopServiceManagerShouldShowStoppedState()
        {
            var buildToSimulate = new Build { BuildNumber = "1", IsBuilding = false, State = Build.BuildState.Failure };
            var buildServiceManagerToTest = SetupMockData(buildToSimulate);

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);
            Thread.Sleep(WaitTime);
            buildServiceManagerToTest.Stop();

            Assert.AreEqual(BuildServiceManager.State.Stopped, buildServiceManagerToTest.CurrentState);

            _threadTask.Wait();
        }

        [TestMethod, Timeout(TestTimeout)]
        public void ServiceManagerShouldShowAsRunning()
        {
            var buildServiceManagerToTest = SetupMockData(new SourceWithDelay(3000));

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);

            Assert.AreEqual(BuildServiceManager.State.Running, buildServiceManagerToTest.CurrentState);

            buildServiceManagerToTest.Stop();

            _threadTask.Wait();
        }

        [TestMethod, Timeout(TestTimeout)]
        public void ServiceManagerWaitingShouldShowAsWaiting()
        {
            var buildServiceManagerToTest = SetupMockData(new User { Build = new Build { State = Build.BuildState.Failure}, UpdateFrequency = 10});

            _threadTask = Task.Factory.StartNew(buildServiceManagerToTest.Run);

            Thread.Sleep(6 * 1000);

            Assert.AreEqual(BuildServiceManager.State.Waiting, buildServiceManagerToTest.CurrentState);

            buildServiceManagerToTest.Stop();

            _threadTask.Wait();
        }

        private BuildServiceManager SetupMockData(Build buildToSimulate)
        {
            var source = new User
                    {
                        Build = buildToSimulate,
                        UpdateFrequency = 1
                    };

            return SetupMockData(source);
        }

        private BuildServiceManager SetupMockData(ISource source)
        {

            var mockLight = SetupMockDeviceController();
            var mockLightFactory = SetupMockLightFactory(mockLight);

            var buildServiceManagerToTest = new BuildServiceManager(source, mockLightFactory.Object);
            return buildServiceManagerToTest;
        }

        private static Mock<ILightFactory> SetupMockLightFactory(Light mockLight)
        {
            var mockLightFactory = new Mock<ILightFactory>(MockBehavior.Strict);

            mockLightFactory.Setup(p => p.CreateLight()).Returns(mockLight);
            return mockLightFactory;
        }

        private Light SetupMockDeviceController(bool isDeviceOpen = true)
        {
            var mockDeviceController = new Mock<IDeviceController>(MockBehavior.Strict);

            //Setup Turn Off Device
            mockDeviceController.Setup(p => p.TurnOffDevice()).Callback(() => _turnOffCount++);
            mockDeviceController.Setup(p => p.TurnOffFlash()).Callback(() => _flashTurnOffCount++);
            mockDeviceController.Setup(p => p.SetDeviceColor(Light.Color.Red)).Callback(() => _lightColorChanged = true);
            mockDeviceController.Setup(p => p.SetDeviceColor(Light.Color.Yellow)).Callback(() => _lightColorChanged = true);
            mockDeviceController.Setup(p => p.SetDeviceColor(Light.Color.Green)).Callback(() => _lightColorChanged = true);

            //Setup Turn On Device
            mockDeviceController.Setup(p => p.TurnOnDevice()).Callback(() => _turnOnCount++);
            mockDeviceController.Setup(p => p.TurnOnFlash()).Callback(() => _flashTurnOnCount++);

            //Setup IsDeviceOpen
            mockDeviceController.Setup(p => p.IsDeviceOpen()).Returns(isDeviceOpen);

            return new Light(mockDeviceController.Object);
        }
    }
}
