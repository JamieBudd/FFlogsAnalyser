using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FFLogsAnalyser.ViewModels
{
    public class RemoveFightsViewModel: Screen, IBaseViewModel, IHandle<AddTimeLineEvent>
    {
        #region Constructor
      
        public RemoveFightsViewModel(IEventAggregator events)
        {
            //Sets the name for the tabcontrol
            DisplayName = "Remove Parse";

            //set up the Event aggregator
            _events = events;
            _events.Subscribe(this);

            TimeLineFights = new ObservableCollection<TimeLineFights>();

            DeleteFight = new RelayCommand(DeleteFightID);
        }

        #endregion

        #region Private Members

        private IEventAggregator _events;

        #endregion

        #region Public Members

        public ObservableCollection<TimeLineFights> TimeLineFights { get; set; }

        public object Param { get; set; }

        public ICommand DeleteFight { get; set; }

        public int SelectedFight { get; set; }

        #endregion


        #region Commands        



        public void Handle(AddTimeLineEvent message)
        {
            TimeLineFights.Add(new TimeLineFights(message.TimeLineFightCode, message.TimeLineID, message.Colour));
        }

        public void DeleteFightID(object param)
        {
            var i = param as TimeLineFights;
            SelectedFight = TimeLineFights.IndexOf(i);
            _events.BeginPublishOnUIThread(new DeleteTimeLineEvent(TimeLineFights[SelectedFight].FightsID));
            TimeLineFights.Remove(TimeLineFights[SelectedFight]);
        }

        #endregion
    }
}
