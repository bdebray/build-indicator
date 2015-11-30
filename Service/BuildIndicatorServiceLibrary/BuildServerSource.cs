using System;
using System.Configuration;
using BuildIndicatorCommon.Model;
using JenkinsConnector;

namespace BuildIndicatorServiceLibrary
{
    class BuildServerSource : ISource
    {
        private readonly IBuildRepository _repository;
        private readonly string _buildPath;

        public string Path
        {
            get { return _buildPath; }
        }

        public BuildServerSource(IBuildRepository repository)
            : this(repository, ConfigurationManager.AppSettings["DefaultBuildPath"])
        {
        }

        public BuildServerSource(IBuildRepository repository, string path)
        {
            _repository = repository;
            _buildPath = path;
        }

        public int UpdateFrequency
        {
            get
            {
                int duration;
                var sleepDurationFromConfiguration = ConfigurationManager.AppSettings["UpdateFrequency"];
                Int32.TryParse(sleepDurationFromConfiguration, out duration);

                return duration;
            }
            set{}
        }

        public Build GetState()
        {
            return GetState(_buildPath);
        }

        public Build GetState(string path)
        {
            return _repository.GetCurrentBuild(path);
        }
    }
}
