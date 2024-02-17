using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Hub.Scripts.Misc
{
    internal class Garbage
    {
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);
        public static async Task Garbage_Collect()
        {
            try
            {
                await Task.Run(() =>
                {
                    EmptyWorkingSet(Process.GetCurrentProcess().Handle);

                    long usedMemory = GC.GetTotalMemory(true);
                });
            }
            catch
            {

            }
        }
    }
}
