using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace GpuMonitor.Models
{
    /// <summary>
    /// GPU の使用率のログを格納するためのクラスです。
    /// </summary>
    public class UsageLogger : BindableBase
    {
        private readonly ObservableCollection<int> usages = new ();
        private int latestUsage;
        private int logCapacity = 60;

        public UsageLogger()
        {
            Usages = new ReadOnlyObservableCollection<int>(usages);
        }

        public ReadOnlyObservableCollection<int> Usages { get; }

        /// <summary>
        /// 最後に AddUsage に入力された使用率を取得します。
        /// </summary>
        public int LatestUsage { get => latestUsage; private set => SetProperty(ref latestUsage, value); }

        /// <summary>
        /// Log の記録数を設定・取得します。デフォルトは 60 で、一分間のログを保持します。
        /// </summary>
        public int LogCapacity { get => logCapacity; set => SetProperty(ref logCapacity, value); }

        /// <summary>
        /// このクラスが保持するリストの先頭に、引数で入力した値を挿入。要素数が `LogCapacity` を超えている場合は、末尾の要素を削除します。
        /// </summary>
        /// <param name="usage">対象のリストの先頭に挿入する値。</param>
        public void AddUsage(int usage)
        {
            usages.Insert(0, usage);
            if (usages.Count > LogCapacity)
            {
                usages.RemoveAt(usages.Count - 1);
            }

            LatestUsage = usage;
        }
    }
}