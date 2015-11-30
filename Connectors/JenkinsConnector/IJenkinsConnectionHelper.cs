using System.Xml.Linq;

namespace JenkinsConnector
{
    public interface IJenkinsConnectionHelper
    {
        XDocument GetXmlResponse(string url);
    }
}