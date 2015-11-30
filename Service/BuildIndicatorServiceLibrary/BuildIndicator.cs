using BuildIndicatorCommon.Model;
using BuildIndicatorControl;
using JenkinsConnector;

namespace BuildIndicatorServiceLibrary
{
    public class BuildIndicator : IBuildIndicator
    {
        private readonly LightFactory _lightFactory = new LightFactory();
        private readonly ISource _buildSource;
        private readonly BuildServiceManager _manager;

        public BuildIndicator()
            : this(new BuildServerSource(new BuildRepository()))
        {
        }

        public BuildIndicator(ISource buildSource)
        {
            _buildSource = buildSource;

            _manager = new BuildServiceManager(_buildSource, _lightFactory);
        }

        public void Run()
        {
            _manager.Run();
        }

        public void ResetBuildSource()
        {
            _manager.BuildSource = _buildSource;
        }

        public void TurnOffAllLights()
        {
            _manager.Reset();
        }
       
        public void TurnOn(Light.Color color)
        {
            var userSource = new User
                {
                    Build = new Build {IsBuilding = false, State = MapState(color)}
                };

            _manager.BuildSource = userSource;
        }

        public void TurnOnFlash(Light.Color color)
        {
            var userSource = new User
            {
                Build = new Build { IsBuilding = true, State = MapState(color) }
            };

            _manager.BuildSource = userSource;
        }

        public void TurnOff(Light.Color color)
        {
            var userSource = new User
                {
                    Build = new Build {IsBuilding = false, State = Build.BuildState.Unknown}
                };

            _manager.BuildSource = userSource;
        }

        public void GetBuildState(string path)
        {
            var buildSource = new BuildServerSource(new BuildRepository(), path);
            _manager.BuildSource = buildSource;
        }

        public string GetBuildServiceState()
        {
            return _manager.CurrentState.ToString();
        }

        public string GetCurrentSource()
        {
            string sourceDetails;

            if (_manager.BuildSource is BuildServerSource)
            {
                sourceDetails = "Build Server";
            }
            else
            {
                sourceDetails = "User";
            }

            if (_manager.BuildSource != null && _manager.BuildSource.Path.Length > 0)
            {
                sourceDetails += ": " + _manager.BuildSource.Path;
            }

            return sourceDetails;
        }

        private static Build.BuildState MapState(Light.Color color)
        {
            var state = Build.BuildState.Unknown;
            switch (color)
            {
                case Light.Color.Green:
                    state = Build.BuildState.Success;
                    break;
                case Light.Color.Yellow:
                    state = Build.BuildState.Unstable;
                    break;
                case Light.Color.Red:
                    state = Build.BuildState.Failure;
                    break;
            }
            return state;
        }

    }
}