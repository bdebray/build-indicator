using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIDIOWINCS;

namespace BuildIndicatorControl
{
    class DelcomLight
    {
        private DelcomDeviceController deviceManager;
        private Byte ledPin;

        public enum Color
        {
            Red,
            Yellow,
            Green
        };

        public DelcomLight(DelcomDeviceController deviceManager, Color lightColor)
        {
            this.deviceManager = deviceManager;
            this.ledPin = this.deviceManager.LightColors[lightColor.ToString()];
        }

        public void TurnOff()
        {
            this.DisableFlash();
            deviceManager.DelcomHIDCommand.MSBData = ledPin;
            deviceManager.TurnOffDevice();
        }

        public void TurnOn(bool flash)
        {
            if (flash)
            {
                this.EnableFlash();
            }
            else
            {
                this.DisableFlash();
            }

            deviceManager.DelcomHIDCommand.LSBData = ledPin;
            deviceManager.TurnOnDevice();
        }

        public void TurnOn()
        {
            this.TurnOn(false);
        }

        public void EnableFlash()
        {
            deviceManager.DelcomHIDCommand.MSBData = ledPin;
            deviceManager.TurnOnFlash();
        }

        public void DisableFlash()
        {
            deviceManager.DelcomHIDCommand.LSBData = ledPin;
            deviceManager.TurnOffFlash();
        }
    }
}
