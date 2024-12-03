using System;
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
        private int usageExcessiveCounter;
        private bool isWarningEnabled;
        private bool isExcessive;

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
        /// 過剰な使用率が一定時間続いた時、警告を出すかどうかを設定・取得します。
        /// </summary>
        public bool IsWarningEnabled { get => isWarningEnabled; set => SetProperty(ref isWarningEnabled, value); }

        /// <summary>
        /// 使用率が過剰だと判定された時、true になります。
        /// </summary>
        public bool IsExcessive { get => isExcessive; private set => SetProperty(ref isExcessive, value); }

        /// <summary>
        /// このクラスが保持するリストの先頭に、引数で入力した値を挿入します。
        /// </summary>
        /// <remarks>
        /// 要素数が `LogCapacity` を超えている場合は、末尾の要素を削除します。<br/>
        /// また、usage が負の数の場合は処理を打ち切ります。
        /// </remarks>
        /// <param name="usage">リストの先頭に挿入する値。</param>
        public void AddUsage(int usage)
        {
            if (usage < 0)
            {
                return;
            }

            usages.Insert(0, usage);
            if (usages.Count > LogCapacity)
            {
                usages.RemoveAt(usages.Count - 1);
            }

            LatestUsage = usage;
            if (usage >= 80)
            {
                usageExcessiveCounter++;
            }
            else
            {
                usageExcessiveCounter = Math.Max(usageExcessiveCounter - 1, 0);
            }

            IsExcessive = usageExcessiveCounter >= 5;
            if (IsExcessive && IsWarningEnabled)
            {
                // 警告を出力する
            }
        }
    }
}