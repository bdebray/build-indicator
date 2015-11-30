using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using BuildIndicatorServiceLibrary;

namespace BuildIndicatorService
{
    public partial class BuildLightService : ServiceBase
    {
        private ServiceHost _host;
        private IBuildIndicator _buildIndicator;

        public BuildLightService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["DebugMode"]))
            {
                //wait 10 seconds to attach debugger
                Thread.Sleep(10000); 
                System.Diagnostics.Debugger.Break();
            }

            _buildIndicator = new BuildIndicator();
            _host = new ServiceHost(_buildIndicator);

            var behavior = _host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.InstanceContextMode = InstanceContextMode.Single;

            _host.Open();
            Task.Factory.StartNew(() => _buildIndicator.Run());
        }

        protected override void OnStop()
        {
            _buildIndicator.TurnOffAllLights();
            _host.Close();
        }
    }
}