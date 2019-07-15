using Caliburn.Micro;
using FFLogsAnalyser.FFlogsClass;
using FFLogsAnalyser.Models;
using GongSolutions.Wpf.DragDrop;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class TimeLineBaseViewModel : BaseViewModel, IDropTarget
    {

        #region Default Constructor

        public TimeLineBaseViewModel(IEventAggregator events)
        {
            //initialise the container
            _events = events;

            GetMousePosition = new RelayCommand(MouseMoveCommand);
            TotalTime = 0;

            //Initialise the viewmodels
            TimeLineMarkers = new TimeLineMarkerViewModel();
            TimeLines = new ObservableCollection<TimeLineViewModel>();
            TimeLineBuffsCollection = new ObservableCollection<TimeLineBuff>();
            TimeLinePopupViewModel = new TimeLinePopupViewModel(_events);
            _timeLineIndex = 0;

            
        }

        #endregion

        #region Private Members

        private IEventAggregator _events;
        private static int _timeLineIndex;
        private static List<ReportEvent> reportEvent = new List<ReportEvent>();
        private static ReportFightID reportFightID;        
        private TimeLineViewModel timeLineView;
        public TimeLinePopupViewModel TimeLinePopupViewModel { get; set; }

        #endregion

        #region Public Members

        /// <summary>
        /// List of Time Markers
        /// </summary>
        public TimeLineMarkerViewModel TimeLineMarkers { get; set; }

        public ObservableCollection<TimeLineBuff> TimeLineBuffsCollection { get; set; }

        





        private double _panelX;
        private double _panelY;

        public double PanelX
        {
            get { return _panelX; }
            set
            {
                if (value.Equals(_panelX)) return;
                _panelX = value;
            }
        }

        public double PanelY
        {
            get { return _panelY; }
            set
            {
                if (value.Equals(_panelY)) return;
                _panelY = value;
            }
        }

        /// <summary>
        /// List of TimeLineViewModels to show the TimeLine
        /// </summary>
        public ObservableCollection<TimeLineViewModel> TimeLines { get; set; }

        public int StartTime;
        public int EndTime;
        public double TotalTime;

        #endregion

        #region Commands

        public async void AddCharacterParseTimeline(int fightID, string reportID)
        {

            //List of TimeLineBuffs to display the name of the Buff
            ObservableCollection<TimeLineBuff> TimeLineBuffs = new ObservableCollection<TimeLineBuff>();

            string reportUrl = Library.report(reportID);

            reportFightID = await Library._download_serialized_json_data<ReportFightID>(reportUrl);
            foreach (Fight item in reportFightID.fights)
            {
                if (item.id == fightID)
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
                        if ((item.type == "removebuff" && item.targetIsFriendly == true && item.ability.name != "Vulnerability Up") || (item.type == "removedebuff" && item.targetIsFriendly == false))
                        {
                                instance.StartTime = StartTime;
                                instance.EndTime = item.timestamp;
                                instance.complete = true;
                        }

                            if ((item.type == "applybuff" && item.targetIsFriendly == true && item.ability.name != "Vulnerability Up") || (item.type == "applydebuff" && item.targetIsFriendly == false))
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

                items.TimeLineGroupIndex = _timeLineIndex;

                int index;
                //finds the index if a timeline is already added
                if (TimeLines.Any(a => a.Name == items.Name))
                {
                    //If it does, get item
                    var file = TimeLines.First(a => a.Name == items.Name);
                    //grab its index
                    index = TimeLines.IndexOf(file);
                }
                else
                {
                    index = -1;
                }

                Enum colour = (TimeLineColours)_timeLineIndex;
                items.Colour = colour.ToString();
                if (index != -1)
                {
                    //if a timeline with the same name isn't in the collection add a new TimeLineViewModel to the end
                    TimeLines.Insert(index, timeLineView = new TimeLineViewModel(items, StartTime, EndTime, colour.ToString()));
                    TimeLineBuffsCollection.Insert(index, items);
                }
                else
                {
                    //if a timeline has the same name insert the TimeLineViewModel into the collection infront of the first instance
                    TimeLines.Add(timeLineView = new TimeLineViewModel(items, StartTime, EndTime, colour.ToString()));
                    TimeLineBuffsCollection.Add(items);
                }

                //Adds the Elements and Markers to the TimeLineView
                timeLineView.AddMarkers();
                timeLineView.AddElement();
                
            }

            _timeLineIndex += 1;
            //Adds the Time Markers or changes them to coincide with the longest battle time
            TimeLineMarkers.AddElements(TotalTime);

            
        }

        /// <summary>
        /// Drag method for moving items in the timeline
        /// </summary>
        /// <param name="dropInfo">the item that is being moved</param>
        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Move;
        }

        /// <summary>
        /// Drop method for moving items in the timeline
        /// </summary>
        /// <param name="dropInfo">the item that is being moved</param>
        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            TimeLineViewModel droptimeline = (TimeLineViewModel)dropInfo.TargetItem;
            TimeLineViewModel sourcetimeline = (TimeLineViewModel)dropInfo.Data;
            int sourceindex = TimeLines.IndexOf(sourcetimeline);
            int dropindex = TimeLines.IndexOf(droptimeline);

            // Moves the TimeLineViewModel and the name of the buff to the dropped location
            TimeLines.Move(sourceindex, dropindex);
            TimeLineBuffsCollection.Move(sourceindex, dropindex);

            
        }

        public ICommand GetMousePosition { get; set; }

        public void MouseMoveCommand()
        {
            _events.PublishOnUIThread(new TrackMouseOverTimeLineEvent(PanelX, PanelY));
        }
        #endregion
    }
}
