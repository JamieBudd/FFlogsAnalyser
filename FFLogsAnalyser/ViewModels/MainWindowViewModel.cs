using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using FFLogsAnalyser;
using PropertyChanged;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]    

    public class MainWindowViewModel : Conductor<object>.Collection.OneActive , IBaseViewModel
    {
        #region Default Constructor

        public MainWindowViewModel(TimeLineBaseViewModel timeLineBaseViewModel, UrlParseViewModel urlParseViewModel, CharacterParseViewModel characterParseViewModel)
        {
            TimeLineBaseViewModel = timeLineBaseViewModel;
            UrlParseViewModel = urlParseViewModel;
            CharacterParseViewModel = characterParseViewModel;
            Setup();
        }

        #endregion

        #region Private Members

        private TimeLineBaseViewModel TimeLineBaseViewModel;
        private UrlParseViewModel UrlParseViewModel;
        private CharacterParseViewModel CharacterParseViewModel;

        #endregion

        #region Public Members

        #endregion

        #region Commands

        public void Setup()
        {
            Items.Add(UrlParseViewModel);
            Items.Add(CharacterParseViewModel);
        }

        #endregion

    }
}
