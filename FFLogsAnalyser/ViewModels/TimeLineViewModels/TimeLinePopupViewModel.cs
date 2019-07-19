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
        #region Constructor

        public TimeLinePopupViewModel(IEventAggregator events)
        {
            //Set up the event aggregator to recieve messages
            _events = events;
            _events.Subscribe(this);
        }

        public TimeLinePopupViewModel()
        {
        }

        #endregion

        #region Private Members
        /// <summary>
        /// The event handler
        /// </summary>
        private IEventAggregator _events;

        #endregion

        #region Public Members

        /// <summary>
        /// The Y position of the mouse
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The X position of the mouse
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The mouse position converted to minutes
        /// </summary>
        public double minute { get; set; }

        /// <summary>
        /// The mouse position converted to seconds
        /// </summary>
        public double second { get; set; }

        /// <summary>
        /// Displays the time where the mouse is on the timeline
        /// </summary>
        public string XToolTip
        {
            get { return $"{minute} : {second.ToString("00")}"; }
        }

        #endregion

        #region Commands
        /// <summary>
        /// The handle to track the mouse position on the timeline
        /// </summary>
        /// <param name="message">contains the X and Y point of the mouse within the timeline</param>
        public void Handle(TrackMouseOverTimeLineEvent message)
        {
            this.X = message.X;
            this.Y = message.Y-10;
            this.minute = Math.Floor((X - 22) / 118);
            this.second = Math.Round(((X - 22) % 118)/2);
        }

        #endregion
    }
}
