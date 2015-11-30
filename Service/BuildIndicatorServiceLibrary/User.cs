using System;
using System.Configuration;
using BuildIndicatorCommon.Model;

namespace BuildIndicatorServiceLibrary
{
    public class User : ISource
    {
        private int _updateFrequency;

        public string Path
        {
            get { return String.Empty; }
        }

        public User()
        {
            var sleepDurationFromConfiguration = ConfigurationManager.AppSettings["UpdateFrequency"];
            Int32.TryParse(sleepDurationFromConfiguration, out _updateFrequency);
        }

        public int UpdateFrequency
        {
            get { return _updateFrequency; } 
            set { _updateFrequency = value; }
        }

        public Build Build { get; set; }

        public Build GetState()
        {
            return Build;
        }

        public Build GetState(string source)
        {
            return Build;
        }
    }
}
