using GpuMonitor.Models;
using Prism.Mvvm;

namespace GpuMonitor.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        public TextWrapper TitleTextWrapper { get; set; } = new ();
    }
}