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


namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineBaseViewModel : BaseViewModel
    {

        #region Default Constructor

        public TimeLineBaseViewModel(Rankings rankings)
        {
            Ranking = rankings;
            TotalTime = 0;

            //Initialise the viewmodels
            TimeLineBuffs = new ObservableCollection<TimeLineBuff>();
            TimeLineMarkers = new TimeLineMarkerViewModel();
            TimeLines = new ObservableCollection<TimeLineViewModel>();
        }

        #endregion


        #region Private Members

        private Rankings Ranking;
        private static List<ReportEvent> reportEvent = new List<ReportEvent>();
        private static ReportFightID reportFightID;        
        private TimeLineViewModel timeLineView;

        #endregion

        #region Public Members

        /// <summary>
        /// List of Time Markers
        /// </summary>
        public TimeLineMarkerViewModel TimeLineMarkers { get; set; }

        /// <summary>
        /// List of TimeLineBuffs to display the name of the Buff
        /// </summary>
        public ObservableCollection<TimeLineBuff> TimeLineBuffs { get; set; }

        /// <summary>
        /// List of TimeLineViewModels to show the TimeLine
        /// </summary>
        public ObservableCollection<TimeLineViewModel> TimeLines { get; set; }

        public int StartTime;
        public int EndTime;
        public double TotalTime;

        #endregion

        #region Commands

        public async void AddCharacterParseTimeline()
        {
            string reportID = Ranking.ReportID;
            string reportUrl = Library.report(reportID);
            int fightID = int.Parse(Ranking.FightID.ToString());
            //List<ReportEvent> reportEvent = await Library.GetBuffData(reportID, reportUrl, reportfightID, fightID);

            reportFightID = await Library._download_serialized_json_data<ReportFightID>(reportUrl);
            foreach (Fight item in reportFightID.fights)
            {
                if (item.id == Ranking.FightID)
                {
                    int i = 0;

                    StartTime = item.start_time;
                    EndTime = item.end_time;
                    TotalTime = Math.Max(TotalTime, (EndTime - StartTime));

                    //get events from FFlogs API and put it in the buff class
                    string reportfighturl = Library.reportbuffs(reportID, StartTime, EndTime);
                    reportEvent.Add(await Library._download_serialized_json_data<ReportEvent>(reportfighturl));
                    try
                    {
                        while (reportEvent[i].nextPageTimestamp != 0)
                        {
                            reportfighturl = Library.reportbuffs(reportID, reportEvent[i].nextPageTimestamp, EndTime);
                            reportEvent.Add(await Library._download_serialized_json_data<ReportEvent>(reportfighturl));
                            i++;
                        }
                    }
                    catch (IndexOutOfRangeException){}
                }
            }

            foreach (var item in reportEvent.Last().events)
            {
                if (item.type == "applybuff" || item.type == "applydebuff" || item.type == "removedebuff" || item.type == "removebuff")
                {
                    int index;
                    //finds the index if a buff is already added
                    if (TimeLineBuffs.Any(a => a.Name == item.ability.name))
                    {
                        //If it does, get item
                        var file = TimeLineBuffs.First(a => a.Name == item.ability.name);
                        //grab its index
                        index = TimeLineBuffs.IndexOf(file);
                    }
                    else
                    {
                        index = -1;
                    }
                    //int index = TimeLineBuffs.FindIndex(a => a.Name.Contains(item.ability.name));

                    if (index == -1)
                    {
                        //if the ability is not already in the list create a new instance of it
                        var timelinebuff = new TimeLineBuff();
                        var instance = new TimeLineBuff.Instance();

                        timelinebuff.Name = item.ability.name;
                        TimeLineBuffs.Add(timelinebuff);

                        //If A buff is used prepull then set the start time to the fight start time and complete the instance
                        if ((item.type == "removebuff" && item.targetIsFriendly == true) || (item.type == "removedebuff" && item.targetIsFriendly == false))
                        {
                                instance.StartTime = StartTime;
                                instance.EndTime = item.timestamp;
                                instance.complete = true;
                        }

                            if ((item.type == "applybuff" && item.targetIsFriendly == true) || (item.type == "applydebuff" && item.targetIsFriendly == false))
                        { 
                                instance.StartTime = item.timestamp;                            
                        }

                        TimeLineBuffs.Last().instance.Add(instance);
                    }
                    else
                    {
                        //if the ability is already in the list then check to see if the last instance has a start and end time.
                        if ((item.type == "applybuff" && item.targetIsFriendly == true) || (item.type == "applydebuff" && item.targetIsFriendly == false))
                        {
                            if (TimeLineBuffs[index].instance.Last().complete == true)
                            {
                                //adds a new instance if the list already has an end time
                                var instance = new TimeLineBuff.Instance();
                                instance.StartTime = item.timestamp;
                                TimeLineBuffs[index].instance.Add(instance);
                            }
                        }

                        //puts the instance EndTime equal to the timestamp then closes the instance
                        if ((item.type == "removebuff" && item.targetIsFriendly == true) || (item.type == "removedebuff" && item.targetIsFriendly == false))
                        {
                            TimeLineBuffs[index].instance.Last().EndTime = item.timestamp;
                            TimeLineBuffs[index].instance.Last().complete = true;
                        }
                    }
                } 
            }

            foreach (var items in TimeLineBuffs)
            {
                //if a buff is still active when the fight ends, add the EndTime as the fight end time then close the instance
                foreach (var instance in items.instance)
                {
                    if (!instance.complete)
                    {
                        instance.EndTime = EndTime;
                        instance.complete = true;
                    }
                }
                TimeLineMarkers.AddElements(TotalTime);

                //Adds the TimeLineViewModel to the collection
                TimeLines.Add(timeLineView = new TimeLineViewModel(items, StartTime, EndTime));

                //Adds the Elements and Markers to the TimeLineView
                timeLineView.AddMarkers();
                timeLineView.AddElement();
                

            }            
        }

        #endregion
    }
}
