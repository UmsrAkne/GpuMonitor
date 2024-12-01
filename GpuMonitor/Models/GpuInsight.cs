using System;
using System.Diagnostics;
using System.Linq;
using NvAPIWrapper.GPU;

namespace GpuMonitor.Models
{
    public static class GpuInsight
    {
        private static PerformanceCounterCategory PerformanceCounterCategory { get; set; } = new PerformanceCounterCategory("GPU Engine");

        public static int GetGpuUsage()
        {
            var gpus = PhysicalGPU.GetPhysicalGPUs();
            var targetGpu =
                gpus.FirstOrDefault(g => g.FullName.Contains("geforce", StringComparison.OrdinalIgnoreCase));

            if (targetGpu != null)
            {
                return targetGpu.UsageInformation.GPU.Percentage;
            }

            return -1;
        }

        public static int GetGpuMemoryUsage()
        {
            var gpus = PhysicalGPU.GetPhysicalGPUs();
            var targetGpu =
                gpus.FirstOrDefault(g => g.FullName.Contains("geforce", StringComparison.OrdinalIgnoreCase));

            if (targetGpu == null)
            {
                return -1;
            }

            var info = targetGpu.MemoryInformation;
            var am = info.AvailableDedicatedVideoMemoryInkB;
            var cm = am - info.CurrentAvailableDedicatedVideoMemoryInkB;
            return (int)(cm / 1024);
        }
    }
}