using System;
using BuildIndicatorCommon.ExceptionHandling;
using BuildIndicatorCommon.Model;
using JenkinsConnector;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace JenkinsConnectorTest
{
    [TestClass]
    public class BuildRepositoryTest
    {
        private Mock<IJenkinsConnectionHelper> _mockHelper;
        private BuildRepository _buildRepositoryToTest;
        private const string MockUrl = "testURL";

        private static Build ExpectedResult
        {
            get
            {
                return new Build
                {
                    BuildNumber = "4",
                    Link = "latestBuildUrl",
                    State = Build.BuildState.Success
                };
            }
        }

        [TestMethod]
        [ExpectedException(typeof(BuildNotFoundException))]
        public void GetCurrentBuildThrowsExceptionWithInvalidResponse()
        {
            SetupMockConnectionHelper(MockUrl, new XDocument());
            _buildRepositoryToTest = new BuildRepository(_mockHelper.Object);
            _buildRepositoryToTest.GetCurrentBuild(MockUrl);
        }

        [TestMethod]
        public void GetCurrentBuildReturnsBuildDetails()
        {
            SetupMockConnectionHelper(MockUrl, SetUpExpectedResultXml());
            SetupMockConnectionHelper(ExpectedResult.ApiLink, new XDocument());

            _buildRepositoryToTest = new BuildRepository(_mockHelper.Object);
            var buildToTest = _buildRepositoryToTest.GetCurrentBuild(MockUrl);

            Assert.AreEqual(ExpectedResult.BuildNumber, buildToTest.BuildNumber);
            Assert.AreEqual(ExpectedResult.Link, buildToTest.Link);
        }

        [TestMethod]
        public void GetCurrentBuildReturnsBuildState()
        {

            SetupMockConnectionHelper(MockUrl, SetUpExpectedResultXml());
            SetupMockConnectionHelper(ExpectedResult.ApiLink, XDocument.Parse("<build><result>Success</result></build>"));
            
            _buildRepositoryToTest = new BuildRepository(_mockHelper.Object);


            var buildToTest = _buildRepositoryToTest.GetCurrentBuild(MockUrl);

            Assert.AreEqual(ExpectedResult.State, buildToTest.State);
            Assert.IsFalse(buildToTest.IsBuilding);
        }

        [TestMethod]
        public void GetCurrentBuildReturnsIsCurrentlyBuilding()
        {

            SetupMockConnectionHelper(MockUrl, SetUpExpectedResultXml());
            SetupMockConnectionHelper(ExpectedResult.ApiLink, XDocument.Parse("<build><building>true</building><result>Unstable</result></build>"));

            _buildRepositoryToTest = new BuildRepository(_mockHelper.Object);

            var buildToTest = _buildRepositoryToTest.GetCurrentBuild(MockUrl);

            Assert.IsTrue(buildToTest.IsBuilding);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownBuildStateException))]
        public void GetCurrentBuildWithUnrecognizedBuildState()
        {

            SetupMockConnectionHelper(MockUrl, SetUpExpectedResultXml());
            SetupMockConnectionHelper(ExpectedResult.ApiLink, XDocument.Parse("<build><result>Succeed</result></build>"));

            _buildRepositoryToTest = new BuildRepository(_mockHelper.Object);


            var buildToTest = _buildRepositoryToTest.GetCurrentBuild(MockUrl);

            Assert.AreEqual(Build.BuildState.Unknown, buildToTest.State);
        }

        //GIVEN the build is currently building
        //GIVEN the build state is not included while it is building
        //WHEN we search for the state of the build
        //THEN we look at the last completed build to get the state
        [TestMethod]
        public void GetCurrentBuildFindsLatestBuildState()
        {
            var lastCompletedBuild = new Build {BuildNumber = "1", Link = "lastCompletedBuildUrl"};
            
            SetupMockConnectionHelper(MockUrl, XDocument.Parse(
                String.Format("<root><lastBuild><number>{0}</number><url>{1}</url></lastBuild><build><number>{2}</number><url>{3}</url></build></root>", ExpectedResult.BuildNumber, ExpectedResult.Link, lastCompletedBuild.BuildNumber, lastCompletedBuild.Link)));
            
            SetupMockConnectionHelper(ExpectedResult.ApiLink, XDocument.Parse("<build><building>true</building></build>"));
            SetupMockConnectionHelper(lastCompletedBuild.ApiLink, XDocument.Parse("<build><result>Failure</result></build>"));

            _buildRepositoryToTest = new BuildRepository(_mockHelper.Object);

            var buildToTest = _buildRepositoryToTest.GetCurrentBuild(MockUrl);

            Assert.AreEqual(Build.BuildState.Failure, buildToTest.State);
        }

        [TestMethod]
        public void GetBuildStateShouldIgnoreCase()
        {
            SetupMockConnectionHelper(MockUrl, SetUpExpectedResultXml(String.Format("<lastBuild><number>{0}</number><url>{1}</url></lastBuild>",
                                                  ExpectedResult.BuildNumber, ExpectedResult.Link)));

            SetupMockConnectionHelper(ExpectedResult.ApiLink, XDocument.Parse("<build><result>FAILURE</result></build>"));

            _buildRepositoryToTest = new BuildRepository(_mockHelper.Object);

            var buildToTest = _buildRepositoryToTest.GetCurrentBuild(MockUrl);

            Assert.AreNotEqual(Build.BuildState.Unknown, buildToTest.State);
            Assert.AreEqual(Build.BuildState.Failure, buildToTest.State);

        }

        private void SetupMockConnectionHelper(string url, XDocument xmlResponse)
        {
            if (_mockHelper == null)
            {
                _mockHelper = new Mock<IJenkinsConnectionHelper>(MockBehavior.Strict);
            }

            _mockHelper.Setup(x => x.GetXmlResponse(url)).Returns(xmlResponse);
        }

        private static XDocument SetUpExpectedResultXml()
        {
            var defaultMockXml = String.Format("<lastBuild><number>{0}</number><url>{1}</url></lastBuild>",
                                                  ExpectedResult.BuildNumber, ExpectedResult.Link);

            return SetUpExpectedResultXml(defaultMockXml);


        }

        private static XDocument SetUpExpectedResultXml(string xml)
        {
            return XDocument.Parse(xml);
        }
    }
}
