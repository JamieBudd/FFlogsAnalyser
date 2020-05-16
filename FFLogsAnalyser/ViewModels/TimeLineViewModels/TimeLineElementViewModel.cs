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
            //set up the variables
            BuffTime = buffTime;
            StartTime = startTime;
            Buffnumber = buffnumber;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Index of the buff contained in Buff Enum
        /// </summary>
        private int Buffnumber;

        #endregion

        #region Public Members

        /// <summary>
        /// Start time of the element
        /// </summary>
        public double StartTime { get; set; }

        /// <summary>
        /// Colour of the element
        /// </summary>
        public string ElementColour
        {
            get
            {
                Enum colour = (BuffColours)Buffnumber;
                return colour.ToString();
            }
        }

        /// <summary>
        /// Length of the element
        /// </summary>
        public double BuffTime { get; set; }

        #endregion

    }
}
