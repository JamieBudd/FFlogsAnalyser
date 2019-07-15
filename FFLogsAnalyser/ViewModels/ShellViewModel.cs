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
        #region Constructor

        public ShellViewModel(MainWindowViewModel mainWindowVM)
        {
            _mainwindowVM = mainWindowVM;
            ActivateItem(mainWindowVM);
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The ViewModel for the Program
        /// </summary>
        private MainWindowViewModel _mainwindowVM;

        #endregion
    }
}
