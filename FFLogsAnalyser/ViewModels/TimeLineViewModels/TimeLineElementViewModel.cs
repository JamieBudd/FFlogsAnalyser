using Caliburn.Micro;
using FFLogsAnalyser.FFlogsClass;
using FFLogsAnalyser.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineElementViewModel : BaseViewModel
    {

        #region Constructor

        public TimeLineElementViewModel(double startTime, double buffTime)
        {
            //Colour = Brushes.Red;
            BuffTime = buffTime;
            StartTime = startTime;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Start time of the element
        /// </summary>
        public double StartTime { get; set; }

        //public Brush Colour { get; set; }

        /// <summary>
        /// Length of the element
        /// </summary>
        public double BuffTime { get; set; }

        #endregion

    }
}
