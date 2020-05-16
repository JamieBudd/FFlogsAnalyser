using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineMarkerViewModel : BaseViewModel
    {

        #region Constructor

        public TimeLineMarkerViewModel()
        {
            Markers = new ObservableCollection<TimeLineMarkerElementViewModel>();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// The displayed time marker
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// List of objects that shows the time markers
        /// </summary>
        public ObservableCollection<TimeLineMarkerElementViewModel> Markers { get; set; }

        #endregion

        #region Private Members

        private double TotalTime;

        #endregion

        public void AddElements(double totalTime)
        {
            TotalTime = totalTime;
            Markers.Clear();
            for(double i = 0; i< (Math.Floor((TotalTime/60000))+1) ; i++)
            {
                double StartTime = (i * 60 * 2);
                string Time = i.ToString("00")+":00";
                Markers.Add(new TimeLineMarkerElementViewModel(Time, StartTime));
            }
        }
    }
}
