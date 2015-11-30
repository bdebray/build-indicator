using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIDIOWINCS;

namespace BuildIndicatorControl
{
    class DelcomDeviceController
    {
        public DelcomHID DelcomHIDController = new DelcomHID();
        public DelcomHID.HidTxPacketStruct DelcomHIDCommand;

        public IDictionary<string, byte> LightColors
            = new Dictionary<string, byte>();

        public bool OpenDevice()
        {
            // Current TID and SID are not supported

            if (DelcomHIDController.Open() == 0)
            {
                UInt32 FamilyCode, SerialNumber, Version, Date, Month, Year, SecurityNumber, ChipType;
                FamilyCode = SerialNumber = Version = Date = Month = Year = SecurityNumber = ChipType = 0;
                DelcomHIDController.GetDeviceInfo(ref FamilyCode, ref SerialNumber, ref Version, ref Date, ref Month, ref Year, ref SecurityNumber, ref ChipType);
                Year += 2000;

                //Console.WriteLine("DeviceName: "+Delcom.GetDeviceName());
                //Console.WriteLine("Device Status: Found. FamilyCode=" + FamilyCode.ToString() + " SerialNumber=" + SerialNumber.ToString() + " Version=" + Version.ToString() + " " + Month.ToString() + "/" + Date.ToString() + "/" + Year.ToString());

                if (SecurityNumber > 0)
                {
                    //Console.WriteLine(" Security Number: " + SecurityNumber.ToString());
                }

                // Del 11-10-2011 - Added test to map LEDs
                if (ChipType == 30 && FamilyCode != 2 && FamilyCode != 3)
                {
                    LightColors.Add("Red", 0x20);
                    LightColors.Add("Yellow", 0x40);
                    LightColors.Add("Green", 0x10);
                    
                    // Port0 - All inputs except P0.7 which is the buzzer, Default all high, All Pullups on, All Interrupts Off
                    DelcomHIDController.SetupPort(0, 0xFF, 0x7F, 0xFF, 0x00);
                    // Port1 - P1.4-7 Outputs, LED drive pins, Default all high, All Pullups on, All Interrupts Off
                    DelcomHIDController.SetupPort(1, 0xFF, 0x0F, 0xFF, 0x00);
                }
                else
                {  // Port1

                    LightColors.Add("Red", 0x02);
                    LightColors.Add("Yellow", 0x04);
                    LightColors.Add("Green", 0x01);
                }

                // Optionally -Enable event counter use that auto switch feature work
                DelcomHIDCommand.MajorCmd = 101;
                DelcomHIDCommand.MinorCmd = 38;
                DelcomHIDCommand.LSBData = 1;
                DelcomHIDCommand.MSBData = 0;
                DelcomHIDController.SendCommand(DelcomHIDCommand);

                return true;
            }
            else
            {
                return false;
                //Console.WriteLine("DeviceName: offine");
                //Console.WriteLine("Error: Unable to open device.");
            }
        }

        public void CloseDevice()
        {
            DelcomHIDController.Close();
            //Console.WriteLine("DeviceName: offine");
            //Console.WriteLine("Device Closed.");
        }

        public Boolean IsDeviceOpen()
        {
            if (DelcomHIDController.IsOpen()) return true;
            //Console.WriteLine("Device not openned. Open device first!\r\nCommand canncelled.","Warning - Device Not Openned!");
            return false;
        }

        public void SetDeviceColor(string color)
        {

        }

        public void TurnOnDevice()
        {
            DelcomHIDCommand.MajorCmd = 101;
            DelcomHIDCommand.MinorCmd = 12;
            DelcomHIDCommand.MSBData = 0;
            //DelcomHIDCommand.LSBData = ledPin;
            DelcomHIDController.SendCommand(DelcomHIDCommand);
        }

        public void TurnOffDevice()
        {
            DelcomHIDCommand.MajorCmd = 101;
            DelcomHIDCommand.MinorCmd = 12;
            DelcomHIDCommand.LSBData = 0;
            //DelcomHIDCommand.MSBData = ledPin;
            DelcomHIDController.SendCommand(DelcomHIDCommand);

        }

        public void TurnOnFlash()
        {
            DelcomHIDCommand.MajorCmd = 101;
            DelcomHIDCommand.MinorCmd = 20;
            DelcomHIDCommand.LSBData = 0;
            //deviceManager.DelcomHIDCommand.MSBData = ledPin;
            DelcomHIDController.SendCommand(DelcomHIDCommand);
        }

        public void TurnOffFlash()
        {
            DelcomHIDCommand.MajorCmd = 101;
            DelcomHIDCommand.MinorCmd = 20;
            DelcomHIDCommand.MSBData = 0;
            DelcomHIDController.SendCommand(DelcomHIDCommand);
        }

    }
}
