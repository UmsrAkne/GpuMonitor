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

        public MainWindowViewModel()
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };

            Timer.Tick += (_, _) =>
            {
                Val = GpuInsight.GetGpuUsage();
            };

            Timer.Start();
        }

        public TextWrapper TitleTextWrapper { get; set; } = new ();

        public int Val
        {
            get => val;
            private set => SetProperty(ref val, value);
        }

        private DispatcherTimer Timer { get; set; }
    }
}