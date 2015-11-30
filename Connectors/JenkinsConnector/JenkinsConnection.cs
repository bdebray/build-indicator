using System;
using System.Net;
using System.Text;

namespace JenkinsConnector
{
    class JenkinsConnection : WebClient
    {
        public JenkinsConnection(string userName, string password)
        {
            var usernamePassword = userName + ":" + password;
            Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes((usernamePassword))));
        }

        public JenkinsConnection()
            : this(JenkinsAccount.UserName, JenkinsAccount.Password)
        {
        }
    }
}
