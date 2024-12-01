using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<int> gpuUsages;

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

            GpuUsages = new ObservableCollection<int>()
            {
                10, 20, 30, 40, 50,
                60, 70, 80, 90, 00,
                80, 90, 80, 70, 80,
                70, 60, 50, 60, 70,
                80, 30, 00, 40, 70,
                50, 80, 50, 90, 50,
                10, 20, 30, 40, 50,
                60, 70, 80, 90, 00,
                80, 90, 80, 70, 80,
                70, 60, 50, 60, 70,
                80, 30, 00, 40, 70,
                50, 80, 50, 90, 50,
            };
        }

        public TextWrapper TitleTextWrapper { get; set; } = new ();

        public int Val
        {
            get => val;
            private set => SetProperty(ref val, value);
        }

        public int Val2 { get => val2; private set => SetProperty(ref val2, value); }

        public ObservableCollection<int> GpuUsages
        {
            get => gpuUsages;
            set => SetProperty(ref gpuUsages, value);
        }

        private DispatcherTimer Timer { get; set; }
    }
}