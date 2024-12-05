using System;
using System.Windows.Threading;
using GpuMonitor.Models;
using Prism.Mvvm;

namespace GpuMonitor.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string gpuName;

        public MainWindowViewModel()
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };

            Timer.Tick += (_, _) =>
            {
                var g = GpuInsight.GetGpuUsage();
                var m = GpuInsight.GetGpuMemoryUsage();
                GpuUsageLogger.AddUsage(g);
                MemoryUsageLogger.AddUsage(m);
            };

            Timer.Start();
            GpuName = GpuInsight.GetGpuName();
        }

        public TextWrapper TitleTextWrapper { get; set; } = new ();

        public UsageLogger GpuUsageLogger { get; set; } = new () { DisplayName = "Gpu Usage", };

        public UsageLogger MemoryUsageLogger { get; set; } = new () { DisplayName = "Memory Usage", };

        public string GpuName { get => gpuName; set => SetProperty(ref gpuName, value); }

        private DispatcherTimer Timer { get; set; }
    }
}