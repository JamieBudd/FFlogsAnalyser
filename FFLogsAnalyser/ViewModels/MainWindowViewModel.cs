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

    public class MainWindowViewModel : Conductor<object>.Collection.OneActive , IBaseViewModel, IHandle<AddParseEvent>
    {
        #region Default Constructor

        public MainWindowViewModel(UrlParseViewModel urlParseViewModel, CharacterParseViewModel characterParseViewModel, IEventAggregator events)
        {
            UrlParseViewModel = urlParseViewModel;
            CharacterParseViewModel = characterParseViewModel;
            TimeLineBaseViewModel = new TimeLineBaseViewModel();
            _events = events;
            _events.Subscribe(this);
            Setup();
        }

        #endregion

        #region Private Members

        private IEventAggregator _events;
        public TimeLineBaseViewModel TimeLineBaseViewModel { get; set; }
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

        public void Handle(AddParseEvent message)
        {            
            TimeLineBaseViewModel.AddCharacterParseTimeline(message.FightID,message.ReportID);
        }

        #endregion

    }
}
