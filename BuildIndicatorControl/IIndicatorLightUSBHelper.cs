using System;
namespace BuildIndicatorControl
{
    public interface IIndicatorLightUSBHelper
    {
        void TurnOffYellow();
        void OpenIndicatorLightDevice();
        void TurnOffGreen();
        void TurnOffRed();
        void TurnOnGreen();
        void TurnOnGreenFlash();
        void TurnOnRed();
        void TurnOnRedFlash();
        void TurnOnYellow();
        void TurnOnYellowFlash();
    }
}
