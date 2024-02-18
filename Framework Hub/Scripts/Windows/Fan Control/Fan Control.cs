using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Framework_Hub.Scripts.Windows.Fan_Control
{
    internal class Fan_Control
    {
        /*
        WARNING: THIS IS JUST PLACEHOLDER TEST CODE FOR FAN CONTROL VIA EC. 
        USE AT YOUR OWN RISK!!! 
        */

        public static int MaxFanSpeed = 100;
        public static int MinFanSpeed = 0;
        public static int MinFanSpeedPercentage = 0;

        public static double FanSpeed = 0;

        public static ushort FanToggleAddress = 0x52;
        public static ushort FanChangeAddress = 0x24;
        public static ushort RegAddress = 0x0802;

        public static bool fanControlEnabled = false;

        public static void UpdateAddresses(ushort reg_data)
        {
            WinRingEC_Management.reg_addr = RegAddress;
            WinRingEC_Management.reg_data = reg_data;
        }

        public static void setFanSpeed(int speedPercentage)
        {
            if (speedPercentage < MinFanSpeedPercentage && speedPercentage > 0)
            {
                speedPercentage = MinFanSpeedPercentage;
            }
            string hexString = speedPercentage.ToString("X");
            byte setValue = (byte)Convert.ToByte(hexString, 16);

            UpdateAddresses(0x0804);
            WinRingEC_Management.ECRamWrite(FanChangeAddress, setValue);
            UpdateAddresses(0x0806);
            WinRingEC_Management.ECRamWrite(FanChangeAddress, setValue);
            FanSpeed = speedPercentage;
        }

        public static void disableFanControl()
        {
            UpdateAddresses(0x0804);
            WinRingEC_Management.ECRamWrite(FanToggleAddress, 0x0);
            UpdateAddresses(0x0806);
            WinRingEC_Management.ECRamWrite(FanToggleAddress, 0x0);
            fanControlEnabled = false;
        }
    }
}
