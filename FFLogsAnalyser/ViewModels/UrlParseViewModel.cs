using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FFLogsAnalyser.ViewModels
{
    public sealed class UrlParseViewModel : Screen, IBaseViewModel
    {
        #region Constructor

        public UrlParseViewModel(IEventAggregator events)
        {
            //Sets the tab header
            DisplayName = "Add Url Parses";

            //initialise the View
            GetParse = new RelayCommand(ShowParse);
            _events = events;
            IsErrorMessageVisible = false;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The event aggregator to send messages to the timeline
        /// </summary>
        private IEventAggregator _events;

        #endregion

        #region Public Members

        /// <summary>
        /// The command to send the data to the timeline
        /// </summary>
        public ICommand GetParse { get; set; }

        /// <summary>
        /// The Url which contains fight data to be used to get information from the API
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Shows or hides the error message
        /// </summary>
        public bool IsErrorMessageVisible { get; set; }

        #endregion

        #region Commands

        public void ShowParse()
        {
            
            string searchfightID = "fight=";
            int searchfightIDindex = Url.IndexOf(searchfightID) + searchfightID.Length;
            string searchreportID = "reports/";
            int searchreportIDindex = Url.IndexOf(searchreportID) + searchreportID.Length;
            int fightID = 0;
            string reportID = "";

            //Find the FightID and reportID from the url data
            try
            {
                fightID = int.Parse(Url.Substring(searchfightIDindex, (Url.IndexOf('&') - searchfightIDindex)));
                reportID = Url.Substring(searchreportIDindex, (Url.IndexOf('#') - searchreportIDindex));
            }
            catch
            {
                //If the data cannot be found show an error message
                IsErrorMessageVisible = true;
            }
            if (reportID != "")
            {
                //if the data is found hide the error message and send the data to the timeline
                IsErrorMessageVisible = false;
            _events.PublishOnUIThread(new AddParseEvent(fightID, reportID));
            }
        }

        #endregion
    }
}
