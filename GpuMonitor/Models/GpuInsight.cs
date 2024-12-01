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
    }
}