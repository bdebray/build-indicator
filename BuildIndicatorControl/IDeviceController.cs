using System;

namespace BuildIndicatorControl
{
    public interface IDeviceController
    {
        bool OpenDevice();
        void CloseDevice();
        Boolean IsDeviceOpen();
        void SetDeviceColor(Light.Color color);
        void TurnOnDevice();
        void TurnOffDevice();
        void TurnOnFlash();
        void TurnOffFlash();
    }
}