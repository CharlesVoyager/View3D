using System;
using System.Diagnostics;

namespace View3D.Utils
{
    public static class RamTools
    {
        public static bool IsRamSizeValid()
        {
            bool valid = true;
            //ulong totalRam = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            ulong availRam = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1024 / 1024;
            //if (availRam < SWSetting.Memory.RemainMin || getCurMemoryUsed() >= (OSTools.Is64BitOperatingSystem() ? SWSetting.Memory.UsedLimit_64bit : SWSetting.Memory.UsedLimit_32bit))
            if (availRam < SWSetting.Memory.RemainMin || getCurMemoryUsed() >= SWSetting.Memory.UsedLimit_64bit)
            {
                valid = false;
            }
            return valid;
        }

        public static uint getCurMemoryUsed()
        {
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            uint totalBytesOfMemoryUsed = Convert.ToUInt16(currentProcess.WorkingSet64 / 1024 / 1024);
            return totalBytesOfMemoryUsed;
        }

    }
}
