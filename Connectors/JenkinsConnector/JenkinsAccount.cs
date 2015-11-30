using System;
using System.Configuration;

namespace JenkinsConnector
{
    public static class JenkinsAccount
    {
        private const string UserNameSetting = "JenkinsUserName";
        private const string PasswordSetting = "JenkinsPassword";
        static JenkinsAccount()
        {
            var jenkinsUserName = ConfigurationManager.AppSettings[UserNameSetting];
            var jenkinsPassword = ConfigurationManager.AppSettings[PasswordSetting];
            if (jenkinsUserName == null)
            {
                throw new Exception("JenkinsUserName setting is not configured");
            }

            if (jenkinsPassword == null)
            {
                throw new Exception("JenkinsPassword is not configured");
            }

            UserName = jenkinsUserName;
            Password = jenkinsPassword;
        }

        public static string UserName { get; set; }

        public static string Password { get; set; }
    }
}
