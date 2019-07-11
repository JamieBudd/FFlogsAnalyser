using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.ViewModels
{
    public class TimeLinePopupViewModel: BaseViewModel, IHandle<TrackMouseOverTimeLineEvent>
    {
        public TimeLinePopupViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
        }

        public TimeLinePopupViewModel()
        {
        }

        private IEventAggregator _events;

        public double Y { get; set; }
        public double X { get; set; }

        public void Handle(TrackMouseOverTimeLineEvent message)
        {
            X = message.X;
            this.Y = message.Y;
        }
    }
}
