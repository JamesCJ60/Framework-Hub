using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Hub.Scripts.Windows.Misc
{
    internal class WinPowerMode
    {
        [DllImport("powrprof.dll", EntryPoint = "PowerSetActiveOverlayScheme")]
        public static extern uint PowerSetActiveOverlayScheme(Guid OverlaySchemeGuid);

        static string highPerformancePowerScheme = "DED574B5-45A0-4F42-8737-46345C09C238";
        static string balancedPowerScheme = "00000000-0000-0000-0000-000000000000";
        static string powerSaverPowerScheme = "961CC777-2547-4F9D-8174-7D86181b8A7A";

        public static void SetWinPowerMode(int mode)
        {
            if(mode == 0) PowerSetActiveOverlayScheme(new Guid(powerSaverPowerScheme.ToLower()));
            else if (mode == 1) PowerSetActiveOverlayScheme(new Guid(balancedPowerScheme.ToLower()));
            else if (mode == 2) PowerSetActiveOverlayScheme(new Guid(highPerformancePowerScheme.ToLower()));
        }
    }
}
