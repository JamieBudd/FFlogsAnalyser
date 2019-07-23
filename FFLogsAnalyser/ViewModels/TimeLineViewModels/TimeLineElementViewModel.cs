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

        public TimeLineElementViewModel(double startTime, double buffTime, int buffnumber)
        {
            //set up the variables from the constructor
            BuffTime = buffTime;
            StartTime = startTime;

            //gets and sets the buff colour
            Enum colour = (BuffColours)buffnumber;
            ElementColour = colour.ToString();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Start time of the element
        /// </summary>
        public double StartTime { get; set; }

        /// <summary>
        /// Colour of the element
        /// </summary>
        public string ElementColour { get; set; }

        /// <summary>
        /// Length of the element
        /// </summary>
        public double BuffTime { get; set; }

        #endregion

    }
}
