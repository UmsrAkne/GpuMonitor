using System;
using System.Windows.Threading;
using GpuMonitor.Models;
using Prism.Mvvm;

namespace GpuMonitor.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private int val;
        private int val2;

        public MainWindowViewModel()
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };

            Timer.Tick += (_, _) =>
            {
                Val = GpuInsight.GetGpuUsage();
                Val2 = GpuInsight.GetGpuMemoryUsage();
            };

            Timer.Start();
        }

        public TextWrapper TitleTextWrapper { get; set; } = new ();

        public int Val
        {
            get => val;
            private set => SetProperty(ref val, value);
        }

        public int Val2 { get => val2; private set => SetProperty(ref val2, value); }

        private DispatcherTimer Timer { get; set; }
    }
}