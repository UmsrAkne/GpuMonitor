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
        private ObservableCollection<int> gpuUsages;
        private ObservableCollection<int> memoryUsages = new ();

        public MainWindowViewModel()
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };

            Timer.Tick += (_, _) =>
            {
                AddItemWithLimit(GpuInsight.GetGpuUsage(), GpuUsages);
                AddItemWithLimit(GpuInsight.GetGpuMemoryUsage(), MemoryUsages);
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

        public ObservableCollection<int> MemoryUsages
        {
            get => memoryUsages;
            set => SetProperty(ref memoryUsages, value);
        }

        public ObservableCollection<int> GpuUsages
        {
            get => gpuUsages;
            set => SetProperty(ref gpuUsages, value);
        }

        private DispatcherTimer Timer { get; set; }

        /// <summary>
        /// 対象のリストの先頭に、引数で入力した値を挿入。要素数が一定値を超えている場合は、末尾の要素を削除します。
        /// </summary>
        /// <param name="v">対象のリストの先頭に挿入する値。</param>
        /// <param name="target">処理の対象とするリスト。</param>
        private void AddItemWithLimit(int v, ObservableCollection<int> target)
        {
            target.Insert(0, v);
            if (target.Count > 60)
            {
                target.RemoveAt(target.Count - 1);
            }
        }
    }
}