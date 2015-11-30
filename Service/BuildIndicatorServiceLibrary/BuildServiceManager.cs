using System;
using System.Threading;
using BuildIndicatorControl;

namespace BuildIndicatorServiceLibrary
{
    public class BuildServiceManager
    {
        private readonly ILightFactory _lightFactory;
        private readonly Light _light;
        private ISource _buildStatusSource;
        private bool _stopped;

        private readonly ManualResetEvent _pausedEvent = new ManualResetEvent(true);
        private readonly ManualResetEvent _waitEvent = new ManualResetEvent(true);

        private Timer _timer;
        private static readonly object Lock = new object();

        public enum State
        {
            Running,
            Waiting,
            Paused,
            Stopped
        }

        public ISource BuildSource
        {
            set
            {
                Pause();
                _buildStatusSource = value;
                SetLight(); //immediately set the light

                UnPause();
            }
            get { return _buildStatusSource; }
        }

        public Light Light
        {
            get { return _light; }
        }

        public State CurrentState
        {
            get
            {
                if (_stopped)
                {
                    return State.Stopped;
                }
                
                if (IsPaused())
                {
                    return State.Paused;
                }
                
                return IsWaiting() ? State.Waiting : State.Running;
            }
        }

        public void UnPause()
        {
            _pausedEvent.Set();
        }

        public void Pause()
        {
            _pausedEvent.Reset();
            StopWaitingToCheckStatus();
        }

        public void Stop()
        {
            _stopped = true;
            UnPause();
            StopWaitingToCheckStatus();
        }

        public void Reset()
        {
            if (_light != null)
            {
                _light.TurnOff(true);
            }
        }

        public BuildServiceManager(ISource source, ILightFactory lightFactory)
        {
            _lightFactory = lightFactory;
            _light = _lightFactory.CreateLight();

            _buildStatusSource = source;

            UnPause();
        }

        public void Run()
        {
            //reset "Stopped" state every time we "Run"
            _stopped = false;

            while (!_stopped)
            {
                _pausedEvent.WaitOne();

                try
                {
                    if (CurrentState.Equals(State.Running))
                    {
                        SetLight();
                    }
                }
                catch (Exception exception)
                {
                    BuildIndicatorExceptionHandler.HandleException(exception, this, _light);
                    continue;
                }

                if (CurrentState.Equals(State.Running))
                {
                    WaitToCheckStatus();
                }
            }
        }

        private void SetLight()
        {
            lock (Lock)
            {
                var buildFromSource = _buildStatusSource.GetState();
                _light.TurnOff(true);
                BuildStateToLightColorMapper.SetLightColorFromBuildState(buildFromSource, _light);
            }
        }

        private void WaitToCheckStatus()
        {
            StartWaitTimer((_buildStatusSource.UpdateFrequency*1000));
            //_waitEvent.WaitOne((_buildStatusSource.UpdateFrequency * 1000));
            _waitEvent.WaitOne();
        }

        
        private void StartWaitTimer(int duration)
        {
            _waitEvent.Reset();
            _timer = new Timer(state => StopWaitingToCheckStatus(), null, duration, Timeout.Infinite);
        }
 
        private void StopWaitingToCheckStatus()
        {
            _waitEvent.Set();
            
            if (_timer != null)
            {
                _timer.Dispose();
            }
             
        }

        private bool IsPaused()
        {
            return !_pausedEvent.WaitOne(0);
        }

        private bool IsWaiting()
        {
            return !_waitEvent.WaitOne(0);
        }
    }
}
