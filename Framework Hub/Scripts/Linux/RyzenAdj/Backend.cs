using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Hub.Scripts.Linux.RyzenAdj
{
    public class RyzenAdj_Backend_Linux
    {
        [DllImport("libryzenadj.so")] public static extern IntPtr init_ryzenadj();
        [DllImport("libryzenadj.so")] public static extern int set_stapm_limit(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_fast_limit(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_slow_limit(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_slow_time(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_stapm_time(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_tctl_temp(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_vrm_current(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_vrmsoc_current(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_vrmmax_current(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_vrmsocmax_current(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_psi0_current(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_psi0soc_current(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_max_gfxclk_freq(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_min_gfxclk_freq(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_max_socclk_freq(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_min_socclk_freq(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_max_fclk_freq(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_min_fclk_freq(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_max_vcn(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_min_vcn(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_max_lclk(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_min_lclk(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_gfx_clk(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_oc_clk(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_per_core_oc_clk(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_oc_volt(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int disable_oc(IntPtr ry);
        [DllImport("libryzenadj.so")] public static extern int enable_oc(IntPtr ry);
        [DllImport("libryzenadj.so")] public static extern int set_prochot_deassertion_ramp(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_apu_skin_temp_limit(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_dgpu_skin_temp_limit(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_apu_slow_limit(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int pbo_scalar(IntPtr ry, [In] uint value);
        [DllImport("libryzenadj.so")] public static extern int set_coall(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_coper(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_cogfx(IntPtr ry, uint value);
        [DllImport("libryzenadj.so")] public static extern int set_power_saving(IntPtr ry);
        [DllImport("libryzenadj.so")] public static extern int set_max_performance(IntPtr ry);

        public static IntPtr ry = init_ryzenadj();
    }
}
