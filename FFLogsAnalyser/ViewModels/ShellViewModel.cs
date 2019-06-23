using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;


namespace FFLogsAnalyser.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private MainWindowViewModel _mainwindowVM;
        public ShellViewModel(MainWindowViewModel mainWindowVM)
        {
            _mainwindowVM = mainWindowVM;
            ActivateItem(mainWindowVM);
        }
    }
}
