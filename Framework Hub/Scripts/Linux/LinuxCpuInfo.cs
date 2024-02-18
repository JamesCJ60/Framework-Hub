using System;
using System.IO;
using System.Text.RegularExpressions;

public class LinuxCpuInfo
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
        string[] cpuInfoLines = File.ReadAllLines(@"/proc/cpuinfo");

        CpuInfoMatch[] cpuInfoMatches =
        {
            new CpuInfoMatch(@"^vendor_id\s+:\s+(.+)", value => VendorId = value),
            new CpuInfoMatch(@"^cpu family\s+:\s+(.+)", value => CpuFamily = int.Parse(value)),
            new CpuInfoMatch(@"^model\s+:\s+(.+)", value => Model = int.Parse(value)),
            new CpuInfoMatch(@"^model name\s+:\s+(.+)", value => ModelName = value),
            new CpuInfoMatch(@"^stepping\s+:\s+(.+)", value => Stepping = int.Parse(value)),
            new CpuInfoMatch(@"^cpu MHz\s+:\s+(.+)", value => MHz = double.Parse(value)),
            new CpuInfoMatch(@"^cache size\s+:\s+(.+)", value => CacheSize = value)
        };

        foreach (string cpuInfoLine in cpuInfoLines)
        {
            foreach (CpuInfoMatch cpuInfoMatch in cpuInfoMatches)
            {
                Match match = cpuInfoMatch.regex.Match(cpuInfoLine);
                if (match.Success)
                {
                    string value = match.Groups[1].Value.Trim();
                    cpuInfoMatch.updateValue(value);
                }
            }
        }
    }

    public class CpuInfoMatch
    {
        public Regex regex;
        public Action<string> updateValue;

        public CpuInfoMatch(string pattern, Action<string> update)
        {
            this.regex = new Regex(pattern, RegexOptions.Compiled);
            this.updateValue = update;
        }
    }
}
