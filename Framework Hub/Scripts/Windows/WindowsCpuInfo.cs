using System;

public class WindowsCpuInfo
{
    public static string VendorId { get; private set; }
    public static int CpuFamily { get; private set; }
    public static int Model { get; private set; }
    public static string ModelName { get; private set; }
    public static int Stepping { get; private set; }
    public static double MHz { get; private set; }
    public static string CacheSize { get; private set; }

    public static void GetValues()
    {
        System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        foreach (System.Management.ManagementObject obj in searcher.Get())
        {
            VendorId = obj["Manufacturer"]?.ToString();
            CpuFamily = int.Parse(obj["Family"]?.ToString() ?? "0");
            ModelName = obj["Name"]?.ToString();
            Stepping = int.Parse(obj["Stepping"]?.ToString() ?? "0");
            MHz = double.Parse(obj["MaxClockSpeed"]?.ToString() ?? "0") / 1000.0; // Convert to GHz
            CacheSize = obj["L2CacheSize"]?.ToString();
            break; // Assuming there's only one CPU
        }
    }
}
