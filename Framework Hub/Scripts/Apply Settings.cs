using Framework_Hub.Scripts.Linux.RyzenAdj;
using Framework_Hub.Scripts.Windows.RyzenAdj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Hub.Scripts
{
    internal class Apply_Settings
    {
        public static void ApplyTDP(int PL1, int PL2)
        {
            //Determine OS
            //Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //Determine CPU vendor
                if (WindowsCpuInfo.ModelName.ToLower().Contains("ryzen"))
                {
                    //Apply slow limit
                    RyzenAdj_Backend_Windows.set_slow_limit(RyzenAdj_Backend_Windows.ry, (uint)(PL1 * 1000));
                    //Apply fast limit
                    RyzenAdj_Backend_Windows.set_fast_limit(RyzenAdj_Backend_Windows.ry, (uint)(PL2 * 1000));
                }
            }
            //Linux
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //Determine CPU vendor
                if (LinuxCpuInfo.ModelName.ToLower().Contains("ryzen"))
                {
                    //Apply slow limit
                    RyzenAdj_Backend_Linux.set_slow_limit(RyzenAdj_Backend_Linux.ry, (uint)(PL1 * 1000));
                    //Apply fast limit
                    RyzenAdj_Backend_Linux.set_fast_limit(RyzenAdj_Backend_Linux.ry, (uint)(PL2 * 1000));
                }
            }
        }

        public static void ApplyCO(int cpuAllCO, int gfxCO)
        {
            //Determine OS
            //Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //Determine CPU vendor
                if (WindowsCpuInfo.ModelName.ToLower().Contains("ryzen"))
                {
                    //Apply all core CO offset
                    RyzenAdj_Backend_Windows.set_coall(RyzenAdj_Backend_Windows.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * cpuAllCO)));
                    //Apply iGPU CO offset
                    RyzenAdj_Backend_Windows.set_cogfx(RyzenAdj_Backend_Windows.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * gfxCO)));
                }
            }
            //Linux
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //Determine CPU vendor
                if (LinuxCpuInfo.ModelName.ToLower().Contains("ryzen"))
                {
                    //Apply all core CO offset
                    RyzenAdj_Backend_Linux.set_coall(RyzenAdj_Backend_Linux.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * cpuAllCO)));
                    //Apply iGPU CO offset
                    RyzenAdj_Backend_Linux.set_cogfx(RyzenAdj_Backend_Linux.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * gfxCO)));
                }
            }
        }
    }
}
