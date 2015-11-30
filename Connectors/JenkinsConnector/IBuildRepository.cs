using BuildIndicatorCommon.Model;

namespace JenkinsConnector
{
    public interface IBuildRepository
    {
        Build GetCurrentBuild(string url);
    }
}
