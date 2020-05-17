using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using PropertyChanged;

namespace FFLogsAnalyser.ViewModels
{

    [AddINotifyPropertyChangedInterface]

    public class ShellViewModel : Conductor<object>
    {
        #region Constructor

        public ShellViewModel(MainWindowViewModel mainWindowVM, IEventAggregator events)
        {
            //initialise menu items
            IncludePersonalBuff = new FullyObservableCollection<ItemMenuPersonalBuffs>();
            IncludePersonalBuff.ItemPropertyChanged += IncludePersonalBuff_ItemPropertyChanged;
            SetupChosenBuffs();
            closeapp = new RelayCommand(CloseApp);
            

            //initialise main window
            _mainwindowVM = mainWindowVM;
            ActivateItem(mainWindowVM);
            _events = events;
  
        }

        #endregion

        #region Private Members

        private IEventAggregator _events;

        /// <summary>
        /// The ViewModel for the Program
        /// </summary>
        private MainWindowViewModel _mainwindowVM;

        #endregion

        #region Public Members


        /// <summary>
        /// Collection which holds the different personal buffs
        /// </summary>
        public FullyObservableCollection<ItemMenuPersonalBuffs> IncludePersonalBuff { get; set; }

        /// <summary>
        /// command which links to <see cref="CloseApp"/> which Closes the Application
        /// </summary>
        public ICommand closeapp { get; set; }

        //public int SelectedBuff { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Adds the personal buffs enum to the collection
        /// </summary>
        public void SetupChosenBuffs()
        {
            
            foreach(PersonalBuffs buff in Enum.GetValues(typeof(PersonalBuffs)))
            {
                IncludePersonalBuff.Add(new ItemMenuPersonalBuffs((int)buff, buff.ToString()));
            }
        }

        /// <summary>
        /// When an item is changed in the collection, send the information to the Library, which lists buffs to be recieved from the API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncludePersonalBuff_ItemPropertyChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            _events.PublishOnUIThread(new AddPersonalBuffsEvent(IncludePersonalBuff));
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        public void CloseApp()
        {
            Application.Current.MainWindow.Close();
        }

        #endregion
    }
}
