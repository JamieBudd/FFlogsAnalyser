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

    public class TimeLineBaseViewModel : BaseViewModel, IDropTarget, IHandle<DeleteTimeLineEvent>
    {

        #region Default Constructor

        public TimeLineBaseViewModel(IEventAggregator events)
        {
            //initialise the container
            _events = events;
            _events.Subscribe(this);

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

        //List of TimeLineBuffs which holds the timeLine data for each buff
        public ObservableCollection<TimeLineBuff> TimeLineBuffs = new ObservableCollection<TimeLineBuff>();

        //list of TimeLineBuffs which shows the name of the buff
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

        /// <summary>
        /// Gets the colour of the current TimeLine
        /// </summary>
        public string TimeLineColour
        {
            get
            {
                //index which loops through the available timeline colours
                int colourpicker = _timeLineIndex % Enum.GetNames(typeof(TimeLineColours)).Length;

                //picks the colour based on the group index of the timeline
                Enum colour = (TimeLineColours)colourpicker;

                //updates the colour
                return colour.ToString();
            }
        }

        #endregion

        #region Commands

        public async Task AddCharacterParseTimeline(int fightID, string reportID)
        {
            //clears the previous TimeLine buffs
            TimeLineBuffs.Clear();

            //gets the events from fflogsAPI
            await GetBuffData(fightID, reportID);

            //adds the events to the TimeLineBuffs list
            AddEventsToTimeLineBuffs();

            //Adds the timeline to the view
            SetupTimeLine();
           
            //sends the timeline data to the removefight tab
            _events.PublishOnUIThread(new AddTimeLineEvent(reportID, _timeLineIndex, TimeLineColour));

            //inreases the timelineindex which refers to the next timeline parse
            _timeLineIndex += 1;
            
            //Adds the Time Markers or changes them to coincide with the longest battle time
            TimeLineMarkers.AddElements(TotalTime);
        }

        /// <summary>
        /// Completes and sets up the timeline view
        /// </summary>
        public void SetupTimeLine()
        {
            foreach (var items in TimeLineBuffs)
            {
                //sets the group index of the timeline
                items.TimeLineGroupIndex = _timeLineIndex;

                //if a buff is still active when the fight ends, add the EndTime as the fight end time then close the instance
                foreach (var instance in items.instance)
                {
                    if (!instance.complete)
                    {
                        instance.EndTime = EndTime;
                        instance.complete = true;
                    }
                }

                int index;
                //finds the index if a timeline is already added
                if (TimeLines.Any(a => a.Name == items.Name))
                {
                    //If it does, get item
                    var tl = TimeLines.First(a => a.Name == items.Name);
                    //grab its index
                    index = TimeLines.IndexOf(tl);
                }
                else
                {
                    index = -1;
                }

                //sets the colour of the timeline
                items.Colour = TimeLineColour;

                if (index != -1)
                {
                    //if a timeline with the same name isn't in the collection add a new TimeLineViewModel to the end
                    TimeLines.Insert(index, timeLineView = new TimeLineViewModel(items, StartTime, EndTime, TotalTime));
                    TimeLineBuffsCollection.Insert(index, items);
                }
                else
                {
                    //if a timeline has the same name insert the TimeLineViewModel into the collection infront of the first instance
                    TimeLines.Add(timeLineView = new TimeLineViewModel(items, StartTime, EndTime, TotalTime));
                    TimeLineBuffsCollection.Add(items);
                }

                //Adds the Elements and Markers to the TimeLineView
                timeLineView.AddMarkers();
                timeLineView.AddElement();

            }
        }

        public async Task GetBuffData(int fightID, string reportID)
        {
            //gets the url
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

                    //get events from FFlogs API and put it into the buff class
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
                    catch (IndexOutOfRangeException) { }
                }
            }
        }

        public void AddEventsToTimeLineBuffs()
        {
            foreach (var item in reportEvent.Last().events)
            {
                if ((item.type == "applybuff" && item.targetIsFriendly == true) ||
                    (item.type == "applydebuff" && item.targetIsFriendly == false) ||
                    (item.type == "removedebuff" && item.targetIsFriendly == false) ||
                    (item.type == "removebuff" && item.targetIsFriendly == true))
                {
                    int index;
                    //finds the index if a buff is already added
                    if (TimeLineBuffs.Any(a => a.Name == item.ability.name))
                    {
                        //If it finds it, get the item
                        var file = TimeLineBuffs.First(a => a.Name == item.ability.name);
                        //grab its index
                        index = TimeLineBuffs.IndexOf(file);
                    }
                    else
                    {
                        index = -1;
                    }

                    if (index == -1)
                    {
                        //if the ability is not already in the list create a new instance of it
                        var timelinebuff = new TimeLineBuff();
                        var instance = new TimeLineBuff.Instance();

                        timelinebuff.Name = item.ability.name;
                        TimeLineBuffs.Add(timelinebuff);

                        //If A buff is used prepull then set the start time to the fight start time and complete the instance
                        if (item.type == "removebuff" || item.type == "removedebuff")
                        {
                            instance.StartTime = StartTime;
                            instance.EndTime = item.timestamp;
                            instance.complete = true;
                        }

                        if (item.type == "applybuff" || item.type == "applydebuff")
                        {
                            instance.StartTime = item.timestamp;
                        }

                        TimeLineBuffs.Last().instance.Add(instance);
                    }
                    else
                    {
                        //if the ability is already in the list then check to see if the last instance has a start and end time.
                        if (item.type == "applybuff" || item.type == "applydebuff")
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
                        if (item.type == "removebuff" || item.type == "removedebuff")
                        {
                            TimeLineBuffs[index].instance.Last().EndTime = item.timestamp;
                            TimeLineBuffs[index].instance.Last().complete = true;
                        }
                    }
                }
            }
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

        /// <summary>
        /// Command to get the mouse position over the timeline
        /// </summary>
        public ICommand GetMousePosition { get; set; }

        /// <summary>
        /// The command used when the mouse goes over the Timeline
        /// </summary>
        public void MouseMoveCommand()
        {
            //sends the mouse position to the TimeLinePopupView
            _events.PublishOnUIThread(new TrackMouseOverTimeLineEvent(PanelX, PanelY));
        }

        /// <summary>
        /// A handle to receive information for when a timeline has been deleted
        /// </summary>
        /// <param name="message">Contains data for a timeline parse</param>
        public void Handle(DeleteTimeLineEvent message)
        {
            //cycles backwards through the timelines
            for(int i = TimeLineBuffsCollection.Count-1; i>=0; i--)
            {
                //checks to see if a timelineview is part of a group
                if (TimeLineBuffsCollection[i].TimeLineGroupIndex == message.FightID)
                {
                    //deletes the timeline group
                    TimeLineBuffsCollection.RemoveAt(i);
                    TimeLines.RemoveAt(i);                    
                }
            }            
        }

        #endregion
    }
}
