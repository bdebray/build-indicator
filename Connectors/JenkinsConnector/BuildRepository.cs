using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BuildIndicatorCommon.Model;
using BuildIndicatorCommon.ExceptionHandling;
using System;

namespace JenkinsConnector
{
    public class BuildRepository : IBuildRepository
    {
        private readonly IJenkinsConnectionHelper _connectionHelper;

        public BuildRepository() : this(new JenkinsConnectionHelper())
        {
            
        }

        public BuildRepository(IJenkinsConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public Build GetCurrentBuild(string url)
        {
            var buildResponseXml = _connectionHelper.GetXmlResponse(url);

            if (!buildResponseXml.Descendants("lastBuild").Any())
            {
                throw new BuildNotFoundException("The Latest Build was not found.");
            }

            var build = buildResponseXml.Descendants("lastBuild")
                                              .Select((latest => new Build
                                                  {
                                                      BuildNumber = GetElementValue(latest.Element("number")),
                                                      Link = GetElementValue(latest.Element("url"))
                                                  })).First();

            GetBuildDetails(build);

            if (build.State.Equals(Build.BuildState.Unknown))
            {
                GetBuildStateForBuildInProgress(build, url);
            }


            return build;
        }

        private void GetBuildStateForBuildInProgress(Build build, string rootUrl)
        {
            if (!build.IsBuilding) return;

            var pastBuilds = GetBuilds(rootUrl);

            foreach (var pastBuild in pastBuilds.Where(pastBuild => !pastBuild.BuildNumber.Equals(build.BuildNumber)))
            {
                GetBuildDetails(pastBuild);

                if (pastBuild.State.Equals(Build.BuildState.Unknown) || pastBuild.State.Equals(Build.BuildState.Aborted))
                {
                    continue;
                }

                build.State = pastBuild.State;

                break;
            }
        }

        private void GetBuildDetails(Build build)
        {
            if (build.ApiLink.Length <= 0) return;

            var lastBuildDetailsResponseXml = _connectionHelper.GetXmlResponse(build.ApiLink);

            var buildingElement = lastBuildDetailsResponseXml.Descendants("building").FirstOrDefault();

            if (buildingElement != null)
            {
                build.IsBuilding = Convert.ToBoolean(buildingElement.Value);
            }

            var resultElement = lastBuildDetailsResponseXml.Descendants("result").FirstOrDefault();
            
            if (resultElement == null) return;
            
            Build.BuildState buildValue;

            if (Enum.TryParse(resultElement.Value.ToLower(), true, out buildValue))
            {
                build.State = buildValue;
            }
            else
            {
                throw new UnknownBuildStateException(resultElement.Value);
            }

        }

        public List<Build> GetBuilds(string url)
        {
            return GetBuilds(url, false);
        }

        public List<Build> GetBuilds(string url, bool loadDetails)
        {
            var buildResponseXml = _connectionHelper.GetXmlResponse(url);
            var builds = new List<Build>();

            if (!buildResponseXml.Descendants("build").Any()) return builds;

            builds = buildResponseXml.Descendants("build")
                                              .Select((build => new Build
                                              {
                                                  BuildNumber = GetElementValue(build.Element("number")),
                                                  Link = GetElementValue(build.Element("url"))
                                              })).ToList();

            if (loadDetails)
            {
                foreach (var build in builds)
                {
                    GetBuildDetails(build);
                }
            }

            return builds;
        }

        private static string GetElementValue(XElement element)
        {
            return element != null ? element.Value : "";
        }
    }
}
