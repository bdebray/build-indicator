using System;
using System.Xml.Linq;

namespace JenkinsConnector
{
    class JenkinsConnectionHelper : IJenkinsConnectionHelper
    {
        private readonly JenkinsConnection _jenkinsClient;

        public JenkinsConnectionHelper() : this(new JenkinsConnection())
        {
            
        }

        public JenkinsConnectionHelper(JenkinsConnection client)
        {
            _jenkinsClient = client;
        }

        public XDocument GetXmlResponse(string url)
        {
            var buildResponse = _jenkinsClient.DownloadString(new Uri(url));
            var buildResponseXml = XDocument.Parse(buildResponse);

            return buildResponseXml;
        }

    }
}
