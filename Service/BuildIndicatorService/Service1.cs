using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BuildIndicatorServiceLibrary;
using System.Threading;
using System.Configuration;

namespace BuildIndicatorService
{
    public partial class Service1 : ServiceBase
    {
        ServiceHost host;

        bool isDebuggingEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["DebugMode"].ToString());

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            /*
            while (isDebuggingEnabled && !Debugger.IsAttached) // Waiting until debugger is attached
            {
                RequestAdditionalTime(1000);  // avoid a timeout
                Thread.Sleep(1000);           // give time to attach debugger
            }

            RequestAdditionalTime(20000); 
            */
            host = new ServiceHost(typeof(BuildIndicatorServiceLibrary.BuildIndicator));
            host.Open();

        }

        protected override void OnStop()
        {
            host.Close();
        }
    }
}
