﻿using Caliburn.Micro;
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

    public class TimeLineBaseViewModel : Conductor<object>.Collection.AllActive, IBaseViewModel
    {

        #region Default Constructor

        public TimeLineBaseViewModel(Rankings rankings)
        {
            ActivateItem(tlmv);
            Ranking = rankings;
            TotalTime = 0;
            
        }

        #endregion


        #region Private Members

        private Rankings Ranking;
        private static List<ReportEvent> reportEvent = new List<ReportEvent>();
        private static ReportFightID reportfightID;
        private List<TimeLineBuff> timeLineBuffs = new List<TimeLineBuff>();
        private TimeLineViewModel tlv;

        private TimeLineMarkerViewModel tlmv = new TimeLineMarkerViewModel();
        #endregion

        #region Public Members

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

            reportfightID = await Library._download_serialized_json_data<ReportFightID>(reportUrl);
            foreach (Fight item in reportfightID.fights)
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
                    catch (IndexOutOfRangeException)
                    {

                    }
                }
            }


            foreach (var item in reportEvent.Last().events)
            {
                if (item.type == "applybuff" || item.type == "applydebuff" || item.type == "removedebuff" || item.type == "removebuff")
                {
                    //finds the index if a buff is already added
                    int index = timeLineBuffs.FindIndex(a => a.Name.Contains(item.ability.name));

                    if (index == -1)
                    {
                        //if the ability is not already in the list create a new instance of it
                        var timelinebuff = new TimeLineBuff();
                        var instance = new TimeLineBuff.Instance();

                        timelinebuff.Name = item.ability.name;
                        timeLineBuffs.Add(timelinebuff);

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

                        timeLineBuffs.Last().instance.Add(instance);
                    }
                    else
                    {
                        //if the ability is already in the list then check to see if the last instance has a start and end time.
                        if ((item.type == "applybuff" && item.targetIsFriendly == true) || (item.type == "applydebuff" && item.targetIsFriendly == false))
                        {
                            if (timeLineBuffs[index].instance.Last().complete == true)
                            {
                                //adds a new instance if the list already has an end time
                                var instance = new TimeLineBuff.Instance();
                                instance.StartTime = item.timestamp;
                                timeLineBuffs[index].instance.Add(instance);
                            }
                        }

                        //puts the instance EndTime equal to the timestamp then closes the instance
                        if ((item.type == "removebuff" && item.targetIsFriendly == true) || (item.type == "removedebuff" && item.targetIsFriendly == false))
                        {
                            timeLineBuffs[index].instance.Last().EndTime = item.timestamp;
                            timeLineBuffs[index].instance.Last().complete = true;
                        }
                    }

                }

                
            }

            

            foreach (var items in timeLineBuffs)
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
                Items.Add(tlv = new TimeLineViewModel(items, StartTime, EndTime));
                tlv.AddElement();
                
            }
            
        }

        #endregion
    }
}
