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
                AddItemWithLimit(GpuInsight.GetGpuUsage());
                Val2 = GpuInsight.GetGpuMemoryUsage();
            };

            Timer.Start();

            GpuUsages = new ObservableCollection<int>()
            {
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
            };
        }

        public TextWrapper TitleTextWrapper { get; set; } = new ();

        public int Val2 { get => val2; private set => SetProperty(ref val2, value); }

        public ObservableCollection<int> GpuUsages
        {
            get => gpuUsages;
            set => SetProperty(ref gpuUsages, value);
        }

        private DispatcherTimer Timer { get; set; }

        private void AddItemWithLimit(int v)
        {
            GpuUsages.Insert(0, v);
            if (GpuUsages.Count > 60)
            {
                GpuUsages.RemoveAt(GpuUsages.Count - 1);
            }
        }
    }
}