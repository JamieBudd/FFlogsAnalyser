using Caliburn.Micro;
using FFLogsAnalyser.FFlogsClass;
using FFLogsAnalyser.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineViewModel : Conductor<object>.Collection.AllActive , IBaseViewModel
    {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TimeLineViewModel(TimeLineBuff timeLineBuff, int startTime, int endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            TimeLineBuff = timeLineBuff;
        }

        #region Private Members

        private int StartTime;
        private int EndTime;
        private TimeLineBuff TimeLineBuff;

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

        #endregion

        #region Commands

        /// <summary>
        /// Adds the buffs to the timeline
        /// </summary>
        public void AddElement()
        {            
            Name = TimeLineBuff.Name;
            TotalTime = Library.ConvertTime(EndTime - StartTime);
            foreach (var item in TimeLineBuff.instance)
            {
                double BuffStartTime = Library.ConvertTime(item.StartTime - StartTime);
                double BuffTime = Library.ConvertTime(item.EndTime - item.StartTime);
                Items.Add(new TimeLineElementViewModel(BuffStartTime, BuffTime));
            }
        }

        #endregion




        

        







        

    }
}
