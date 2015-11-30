using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildIndicatorControl
{
    public class DelcomIndicatorLightUSBHelper : IIndicatorLightUSBHelper
    {
        DelcomDeviceController controller = new DelcomDeviceController();

        DelcomLight greenLight;
        DelcomLight redLight;
        DelcomLight yellowLight;

        public void OpenIndicatorLightDevice()
        {
            if (controller.OpenDevice())
            {
                redLight = new DelcomLight(controller, DelcomLight.Color.Red);
                yellowLight = new DelcomLight(controller, DelcomLight.Color.Yellow);
                greenLight = new DelcomLight(controller, DelcomLight.Color.Green);
            }
        }

        public void Close()
        {
            controller.CloseDevice();
        }

        private Boolean IsPresent()
        {
            if (controller.IsDeviceOpen()) return true;
            return false;
        }


     
        public void TurnOffGreen()
        {
            if (!IsPresent()) return;

            greenLight.TurnOff();
        }

        public void TurnOnGreen()
        {
            if (!IsPresent()) return;

            greenLight.TurnOn();
        }

        public void TurnOnGreenFlash()
        {
            if (!IsPresent()) return;

            greenLight.TurnOn(true);
        }


        public void TurnOffRed()
        {
            if (!IsPresent()) return;

            redLight.TurnOff();
        }

        public void TurnOnRed()
        {
            if (!IsPresent()) return;

            redLight.TurnOn();
        }

        public void TurnOnRedFlash()
        {
            if (!IsPresent()) return;

            redLight.TurnOn(true);
        }


        public void TurnOffYellow()
        {
            if (!IsPresent()) return;

            yellowLight.TurnOff();
        }

        public void TurnOnYellow()
        {
            if (!IsPresent()) return;

            yellowLight.TurnOn();
        }

        public void TurnOnYellowFlash()
        {
            if (!IsPresent()) return;

            yellowLight.TurnOn(true);
        }

    }
}
