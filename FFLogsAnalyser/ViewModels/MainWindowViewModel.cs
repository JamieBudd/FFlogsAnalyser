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

    public class MainWindowViewModel : Conductor<object>.Collection.OneActive, IBaseViewModel, IHandle<AddParseEvent>
    {
        #region Default Constructor

        public MainWindowViewModel(UrlParseViewModel urlParseViewModel, CharacterParseViewModel characterParseViewModel, IEventAggregator events, TimeLineBaseViewModel timeLineBaseViewModel)
        {
            //initialise the ViewModels
            UrlParseViewModel = urlParseViewModel;
            CharacterParseViewModel = characterParseViewModel;
            TimeLineBaseViewModel = timeLineBaseViewModel;

            //listen for hadlers
            _events = events;
            _events.Subscribe(this);

            //setup the tab control
            Setup();
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The event aggregator to receive messages
        /// </summary>
        private IEventAggregator _events;

        /// <summary>
        /// The View for adding a url parse
        /// </summary>
        private UrlParseViewModel UrlParseViewModel;

        /// <summary>
        /// The View for adding a character ranking parse
        /// </summary>
        private CharacterParseViewModel CharacterParseViewModel;

        #endregion

        #region Public Members

        /// <summary>
        /// The View for showing the timeline
        /// </summary>
        public TimeLineBaseViewModel TimeLineBaseViewModel { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Adds the ViewModels to the Tab control
        /// </summary>
        public void Setup()
        {
            Items.Add(UrlParseViewModel);
            Items.Add(CharacterParseViewModel);

        }

        /// <summary>
        /// Adds a timeline if data is received
        /// </summary>
        /// <param name="message"></param>
        public void Handle(AddParseEvent message)
        {
            TimeLineBaseViewModel.AddCharacterParseTimeline(message.FightID, message.ReportID);
        }

        #endregion
    }
}
