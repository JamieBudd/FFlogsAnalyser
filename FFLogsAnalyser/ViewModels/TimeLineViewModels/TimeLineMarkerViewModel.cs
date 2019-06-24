using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineMarkerViewModel : Conductor<object>.Collection.AllActive, IBaseViewModel
    {

        #region Constructor

        public TimeLineMarkerViewModel()
        {
            
        }

        #endregion

        #region Public Members

        public string Time { get; set; }

        #endregion

        #region Private Members

        private double TotalTime;

        #endregion

        public void AddElements(double totalTime)
        {
            TotalTime = totalTime;
            Items.Clear();
            for(double i = 0; i< (Math.Floor((TotalTime/60000))+1) ; i++)
            {
                double StartTime = (i * 60) * 2;
                string Time = "0" + i + ":00";
                Items.Add(new TimeLineMarkerElementViewModel(Time, StartTime));
            }
        }
    }
}
