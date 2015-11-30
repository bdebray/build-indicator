using System;
using BuildIndicatorCommon.ExceptionHandling;

namespace BuildIndicatorControl
{
    public class Light
    {
        public enum Color
        {
            Red,
            Yellow,
            Green
        };

        private Color _lightColor;
        private readonly IDeviceController _deviceController;

        public Color LightColor
        {
            get { return _lightColor; }
            set 
            { 
                _lightColor = value;
                _deviceController.SetDeviceColor(_lightColor);
                //_ledPin = _deviceController.LightColors[LightColor.ToString()];
            }
        }

        public Light(IDeviceController deviceController)
        {
            _deviceController = deviceController;
        }

        public Light(IDeviceController deviceController, Color lightColor)
        {
            _deviceController = deviceController;
            LightColor = lightColor;
        }

        public void TurnOff(bool turnOffAllLightColors = false)
        {
            DisableFlash();

            if (turnOffAllLightColors)
            {
                var lightColors = Enum.GetValues(typeof (Color));

                foreach (var lightColor in lightColors)
                {
                    LightColor = (Color)lightColor;
                    _deviceController.TurnOffDevice();
                }
            }
            else
            {
                _deviceController.TurnOffDevice();
            }
        }

        public void TurnOn(bool flash)
        {
            if (!IsDeviceOpen())
            {
                throw new DeviceNotConnectedException();
            }

            if (flash)
            {
                EnableFlash();
            }
            else
            {
                DisableFlash();
            }

            _deviceController.TurnOnDevice();
        }

        public void TurnOn()
        {
            TurnOn(false);
        }

        public void EnableFlash()
        {
            _deviceController.TurnOnFlash();
        }

        public void DisableFlash()
        {
            _deviceController.TurnOffFlash();
        }

        public bool IsDeviceOpen()
        {
            return _deviceController.IsDeviceOpen();
        }

        public void Close()
        {
            _deviceController.CloseDevice();
        }
    }
}