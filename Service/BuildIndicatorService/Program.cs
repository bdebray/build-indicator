using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace BuildIndicatorService
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                var parameter = string.Concat(args);
                switch (parameter)
                {
                    case "-install":
                        ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
                        break;
                    case "-uninstall":
                        ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                    default:
                        throw new Exception("Invalid command (" + parameter + ")");
                }
            }
            {
                var servicesToRun = new ServiceBase[]
                {
                    new BuildLightService()
                };

                ServiceBase.Run(servicesToRun);
            }
        }
    }
}