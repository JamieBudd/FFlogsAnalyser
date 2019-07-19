
using System;
using System.Windows.Input;

namespace FFLogsAnalyser.ViewModels
{
    public class RelayCommand : ICommand
    {
        #region Constructor

        public RelayCommand(Action<object> action)
        {
            mActionwithparam = action;
        }

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Private members
        /// <summary>
        /// the action to run
        /// </summary>
        private Action mAction;

        /// <summary>
        /// The action to run if it includes a parameter
        /// </summary>
        readonly Action<object> mActionwithparam;

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

        #region Methods
        /// <summary>
        /// runs the action passed in
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                mAction();
            }
            else
            {
                mActionwithparam(parameter);
            }
        }

        #endregion
    }
}
