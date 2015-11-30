using System;
using System.Collections.Generic;
using HIDIOWINCS;

namespace BuildIndicatorControl
{
    public class DelcomDeviceController : IDeviceController
    {
        private Byte _ledPin;

        private DelcomHID.HidTxPacketStruct _delcomHidCommand;
        private readonly DelcomHID _delcomHidController = new DelcomHID();

        public IDictionary<Light.Color, byte> LightColors
            = new Dictionary<Light.Color, byte>();

        public bool OpenDevice()
        {
            // Current TID and SID are not supported

            if (_delcomHidController.Open() != 0)
            {
                return false;
                //Console.WriteLine("DeviceName: offine");
                //Console.WriteLine("Error: Unable to open device.");
            }

            UInt32 serialNumber, version, date, month, year, securityNumber, chipType;
                
            var familyCode = serialNumber = version = date = month = year = securityNumber = chipType = 0;
                
            _delcomHidController.GetDeviceInfo(ref familyCode, ref serialNumber, ref version, ref date, ref month,
                                              ref year, ref securityNumber, ref chipType);
/*
                year += 2000;
*/

            //Console.WriteLine("DeviceName: "+Delcom.GetDeviceName());
            //Console.WriteLine("Device Status: Found. FamilyCode=" + FamilyCode.ToString() + " SerialNumber=" + SerialNumber.ToString() + " Version=" + Version.ToString() + " " + Month.ToString() + "/" + Date.ToString() + "/" + Year.ToString());

            if (securityNumber > 0)
            {
                //Console.WriteLine(" Security Number: " + SecurityNumber.ToString());
            }

            // Del 11-10-2011 - Added test to map LEDs
            if (chipType == 30 && familyCode != 2 && familyCode != 3)
            {
                LightColors.Add(Light.Color.Red, 0x20);
                LightColors.Add(Light.Color.Yellow, 0x40);
                LightColors.Add(Light.Color.Green, 0x10);

                // Port0 - All inputs except P0.7 which is the buzzer, Default all high, All Pullups on, All Interrupts Off
                _delcomHidController.SetupPort(0, 0xFF, 0x7F, 0xFF, 0x00);
                // Port1 - P1.4-7 Outputs, LED drive pins, Default all high, All Pullups on, All Interrupts Off
                _delcomHidController.SetupPort(1, 0xFF, 0x0F, 0xFF, 0x00);
            }
            else
            {
                // Port1

                LightColors.Add(Light.Color.Red, 0x02);
                LightColors.Add(Light.Color.Yellow, 0x04);
                LightColors.Add(Light.Color.Green, 0x01);
            }

            // Optionally -Enable event counter use that auto switch feature work
            _delcomHidCommand.MajorCmd = 101;
            _delcomHidCommand.MinorCmd = 38;
            _delcomHidCommand.LSBData = 1;
            _delcomHidCommand.MSBData = 0;
            _delcomHidController.SendCommand(_delcomHidCommand);

            return true;
        }

        public void CloseDevice()
        {
            _delcomHidController.Close();
            //Console.WriteLine("DeviceName: offine");
            //Console.WriteLine("Device Closed.");
        }

        public Boolean IsDeviceOpen()
        {
            return _delcomHidController.IsOpen();
            //Console.WriteLine("Device not openned. Open device first!\r\nCommand canncelled.","Warning - Device Not Openned!");
        }

        public void SetDeviceColor(Light.Color color)
        {
            _ledPin = LightColors[color];
        }

        public void TurnOnDevice()
        {
            _delcomHidCommand.MajorCmd = 101;
            _delcomHidCommand.MinorCmd = 12;
            _delcomHidCommand.MSBData = 0;
            _delcomHidCommand.LSBData = _ledPin;
            _delcomHidController.SendCommand(_delcomHidCommand);
        }

        public void TurnOffDevice()
        {
            _delcomHidCommand.MajorCmd = 101;
            _delcomHidCommand.MinorCmd = 12;
            _delcomHidCommand.LSBData = 0;
            _delcomHidCommand.MSBData = _ledPin;
            _delcomHidController.SendCommand(_delcomHidCommand);
        }

        public void TurnOnFlash()
        {
            _delcomHidCommand.MajorCmd = 101;
            _delcomHidCommand.MinorCmd = 20;
            _delcomHidCommand.LSBData = 0;
            _delcomHidCommand.MSBData = _ledPin;
            _delcomHidController.SendCommand(_delcomHidCommand);
        }

        public void TurnOffFlash()
        {
            _delcomHidCommand.MajorCmd = 101;
            _delcomHidCommand.MinorCmd = 20;
            _delcomHidCommand.MSBData = 0;
            _delcomHidCommand.LSBData = _ledPin;
            _delcomHidController.SendCommand(_delcomHidCommand);
        }
    }
}