using System;
using System.Linq;
using NvAPIWrapper.GPU;

namespace GpuMonitor.Models
{
    public static class GpuInsight
    {
        /// <summary>
        /// PCに搭載されているGPUの使用率を取得します。
        /// </summary>
        /// <returns>
        /// 結果は `0-100` の間の整数で取得されます。<br/>
        /// ただし、GPU の情報が取得できなかった場合は -1 を返します。
        /// </returns>
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

        /// <summary>
        /// PCに搭載されているGPUのメモリ使用量を取得します。
        /// </summary>
        /// <returns>
        /// 結果は Mb 単位の整数で取得されます。<br/>
        /// ただし、GPU の情報が取得できなかった場合は -1 を返します。
        /// </returns>
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