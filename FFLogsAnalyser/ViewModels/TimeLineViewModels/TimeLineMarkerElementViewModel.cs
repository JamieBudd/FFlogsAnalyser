using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    class TimeLineMarkerElementViewModel
    {
        #region Constructor

        public TimeLineMarkerElementViewModel(string time, double startTime)
        {
            Time = time;
            StartTime = startTime;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Time displayed in an element
        /// </summary>
        public string Time { get; set; }
        public double StartTime { get; set; }

        #endregion
    }
}
