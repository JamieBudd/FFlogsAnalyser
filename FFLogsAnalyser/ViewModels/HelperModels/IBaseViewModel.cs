using System.ComponentModel;

namespace FFLogsAnalyser.ViewModels
{
    public interface IBaseViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}