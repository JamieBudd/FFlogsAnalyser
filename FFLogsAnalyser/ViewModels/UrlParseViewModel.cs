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
        public UrlParseViewModel(IEventAggregator events)
        {
            //Sets the tab header
            DisplayName = "Url Parses";
            GetParse = new RelayCommand(ShowParse);
            _events = events;
        }

        #region Private Members

        private IEventAggregator _events;

        #endregion

        #region Public Members

        public ICommand GetParse { get; set; }
        public string Url { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Commands

        public void ShowParse()
        {
            string searchfightID = "fight=";
            int searchfightIDindex = Url.IndexOf(searchfightID) + searchfightID.Length;
            string searchreportID = "reports/";
            int searchreportIDindex = Url.IndexOf(searchreportID) + searchreportID.Length;

            int fightID = int.Parse(Url.Substring(searchfightIDindex, (Url.IndexOf('&') - searchfightIDindex)));
            string reportID = Url.Substring(searchreportIDindex, (Url.IndexOf('#') - searchreportIDindex));

            _events.PublishOnUIThread(new AddParseEvent(fightID, reportID));
        }

        #endregion
    }
}
