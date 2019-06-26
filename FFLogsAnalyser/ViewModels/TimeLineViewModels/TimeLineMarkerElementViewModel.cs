using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineMarkerElementViewModel : BaseViewModel
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

        /// <summary>
        /// StartTime of the element
        /// </summary>
        public double StartTime { get; set; }

        #endregion
    }
}
