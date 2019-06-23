using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.ViewModels
{
    /// <summary>
    /// Adds a Base ViewModel class to implement INotifyPropertyChanged as needed.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged, IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
