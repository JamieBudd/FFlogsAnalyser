using Caliburn.Micro;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

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


        public double minute { get; set; }
        public double second { get; set; }
        private string _xtooltip;

        public string XToolTip
        {
            get { return $"{minute} : {second.ToString("00")}"; }
            set { _xtooltip = value; }
        }


        private IEventAggregator _events;

        public double Y { get; set; }
        public double X { get; set; }

        public void Handle(TrackMouseOverTimeLineEvent message)
        {
            this.X = message.X;
            this.Y = message.Y-10;
            this.minute = Math.Floor((X - 22) / 118);
            this.second = Math.Round(((X - 22) % 118)/2);
        }
    }
}
