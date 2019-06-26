﻿using Caliburn.Micro;
using FFLogsAnalyser.FFlogsClass;
using FFLogsAnalyser.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineViewModel : BaseViewModel
    {

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TimeLineViewModel(TimeLineBuff timeLineBuff, int startTime, int endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            TimeLineBuff = timeLineBuff;

            //Initialise the collections for the viewmodels
            Elements = new ObservableCollection<TimeLineElementViewModel>();
            Rect = new ObservableCollection<TimeLineDivider>();
            
        }

        #endregion

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

        /// <summary>
        /// List of Lines to divide the timeline into sections
        /// </summary>
        public ObservableCollection<TimeLineDivider> Rect { get; set; }

        /// <summary>
        /// List of buffs to be shown on the timeline
        /// </summary>
        public ObservableCollection<TimeLineElementViewModel> Elements { get; set; }

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
                Elements.Add(new TimeLineElementViewModel(BuffStartTime, BuffTime));
            }
        }

        /// <summary>
        /// Adds the Lines which divide the timeline into minutes
        /// </summary>
        public void AddMarkers()
        {
            double Minutes = Math.Floor(((TotalTime/2) / 60))+1;
            for (double i = 1; i < Minutes; i++)
            {
                double StartTime = ((i * 60) * 2)-1;
                TimeLineDivider tld = new TimeLineDivider();
                tld.StartTime = StartTime;
                Rect.Add(tld);
            }
        }
        #endregion

    }
}
