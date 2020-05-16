using Caliburn.Micro;
using FFLogsAnalyser.FFlogsClass;
using FFLogsAnalyser.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineViewModel : BaseViewModel
    {

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TimeLineViewModel(TimeLineBuff timeLineBuff, int startTime, int endTime, double totaltime)
        {
            StartTime = startTime;
            EndTime = endTime;
            TimeLineBuffs = timeLineBuff;
            TotalTime = Library.ConvertTime(totaltime);

            //Initialise the collections for the viewmodels
            Elements = new ObservableCollection<TimeLineElementViewModel>();
            Rect = new ObservableCollection<TimeLineDivider>();
            
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The start time of the fight
        /// </summary>
        private int StartTime;

        /// <summary>
        /// The end time of the fight
        /// </summary>
        private int EndTime;

        /// <summary>
        /// Class which holds data for the timeline
        /// </summary>
        private TimeLineBuff TimeLineBuffs;

        #endregion

        #region Public Members
        /// <summary>
        /// Total Length of the fight
        /// </summary>
        public double TotalTime { get; set; }

        /// <summary>
        /// Ability Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of Lines to divide the timeline into sections
        /// </summary>
        public ObservableCollection<TimeLineDivider> Rect { get; set; }

        /// <summary>
        /// List of buffs to be shown on the timeline
        /// </summary>
        public ObservableCollection<TimeLineElementViewModel> Elements { get; set; }

        public string TimeLineColour
        {
            get
            {
                return TimeLineBuffs.Colour;
            }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Adds the buffs to the timeline
        /// </summary>
        public void AddElement()
        {            
            //Name = TimeLineBuff.Name;
            string BuffName = TimeLineBuffs.Name.Replace(' ', '_');
            if (BuffName == "Vulnerability_Up")
            {
                BuffName = "Trick_Attack";
            }
            TotalTime = Library.ConvertTime(EndTime - StartTime)*2;
            foreach (var item in TimeLineBuffs.instance)
            {
                int BuffNumber = (int)Enum.Parse(typeof(Buffs), BuffName);
                double BuffStartTime = Library.ConvertTime(item.StartTime - StartTime)*2;
                double BuffTime = Library.ConvertTime(item.EndTime - item.StartTime)*2;
                Elements.Add(new TimeLineElementViewModel(BuffStartTime, BuffTime, BuffNumber));
            }
        }

        /// <summary>
        /// Adds the Lines which divide the timeline into minutes
        /// </summary>
        public void AddMarkers()
        {
            double Minutes = Math.Floor((TotalTime / 60))+1;
            for (double i = 1; i < Minutes; i++)
            {
                double StartTime = ((i * 60) * 2);
                TimeLineDivider tld = new TimeLineDivider();
                tld.StartTime = StartTime;
                Rect.Add(tld);
            }
        }
        #endregion

    }
}
