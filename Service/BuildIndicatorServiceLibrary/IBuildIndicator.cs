using System.ServiceModel;
using BuildIndicatorControl;

namespace BuildIndicatorServiceLibrary
{
    [ServiceContract]
    public interface IBuildIndicator
    {
        [OperationContract]
        void TurnOn(Light.Color color);

        [OperationContract]
        void TurnOff(Light.Color color);

        [OperationContract]
        void TurnOnFlash(Light.Color color);

        [OperationContract]
        void TurnOffAllLights();

        [OperationContract]
        void GetBuildState(string path);

        [OperationContract]
        void ResetBuildSource();

        [OperationContract]
        void Run();

        [OperationContract]
        string GetBuildServiceState();

        [OperationContract]
        string GetCurrentSource();
    }
}