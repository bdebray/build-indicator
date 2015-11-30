using System.Threading;
using BuildIndicatorCommon.Model;
using BuildIndicatorServiceLibrary;

namespace BuildIndicatorServiceLibraryTest
{
    class SourceWithDelay : ISource
    {
        private readonly int _delayTime;
        public int UpdateFrequency { get; set; }

        public string Path { get; private set; }

        public Build GetState()
        {
            Thread.Sleep(_delayTime);
            return new Build { IsBuilding = false, State = Build.BuildState.Failure };
        }

        public Build GetState(string path)
        {
            throw new System.NotImplementedException();
        }

        public SourceWithDelay(int delayTime)
        {
            _delayTime = delayTime;
        }
    }
}
