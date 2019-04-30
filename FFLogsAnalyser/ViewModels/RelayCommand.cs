
using System;
using System.Windows.Input;

namespace FFLogsAnalyser
{
    public class RelayCommand : ICommand
    {
        #region Private members
        /// <summary>
        /// the action to run
        /// </summary>
        private Action mAction;
        #endregion

        #region Public members

        /// <summary>
        /// The event that is fired when <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// The relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        #endregion

        #region Constructor

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Methods
        /// <summary>
        /// runs the action passed in
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}
