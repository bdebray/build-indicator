using BuildIndicatorCommon.Model;

namespace BuildIndicatorServiceLibrary
{
    public interface ISource
    {
        int UpdateFrequency { get; set; }

        string Path { get; }

        Build GetState();
        Build GetState(string path);
    }
}
