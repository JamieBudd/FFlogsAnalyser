using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using FFLogsAnalyser;
using PropertyChanged;

namespace FFLogsAnalyser.ViewModels
{
    [AddINotifyPropertyChangedInterface]    

    public class MainWindowViewModel : Conductor<object>.Collection.OneActive , IBaseViewModel
    {
        #region Default Constructor

        public MainWindowViewModel(TimeLineViewModel timeLineVM)
        {
            //sets the default value of _regionselected and Enumsource
            _regionselected = "EU";
            EnumSource = Enum.GetValues(typeof(ServerEU));
            GetParse = new RelayCommand(ShowParse);
            _timelineVM = timeLineVM;
        }

        #endregion

        #region Private Members

        private TimeLineViewModel _timelineVM;
        private string _serverselected;
        private string _regionselected;
        private string _firstname;
        private string _lastname;

        #endregion

        #region Public Members

        public int SelectedParse { get; set; }

        /// <summary>
        /// The first name that is inputted by the user
        /// </summary>
        public string FirstName
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        /// <summary>
        /// The last name that is inputted by the user
        /// </summary>
        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        /// <summary>
        /// Property which holds the Enum that is selected
        /// </summary>
        public Array EnumSource { get; set; }

        /// <summary>
        /// Casts the Region Enum to an IEnumarable to be used as a binding property in the UI
        /// </summary>
        public IEnumerable<Region> Regions
        {
            get
            {
                return Enum.GetValues(typeof(Region)).Cast<Region>();
            }
        }
        /// <summary>
        /// Gets the selected Server
        /// </summary>
        public string ServerSelected
        {
            get { return _serverselected; }
            set { _serverselected = value; }
        }

        /// <summary>
        /// Gets the selected Region
        /// </summary>
        public string RegionSelected
        {
            get { return _regionselected; }
            set
            {
                _regionselected = value;
                switch (value)
                {
                    case "EU":
                        //Copies the Enum "ServerEU" to the EnumSource property
                        EnumSource = Enum.GetValues(typeof(ServerEU));
                        break;

                    case "NA":
                        //Copies the Enum "ServerNA" to the EnumSource property
                        EnumSource = Enum.GetValues(typeof(ServerNA));
                        break;

                    case "JP":
                        //Copies the Enum "ServerJP" to the EnumSource property
                        EnumSource = Enum.GetValues(typeof(ServerJP));
                        break;
                }
            }
        }

        /// <summary>
        /// Shows the list of Character Parses based on user input
        /// </summary>
        public List<Rankings> CharacterParses { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Uses the inputted data to get Character parses from fflogsAPI
        /// </summary>
        /// <param name="firstName">Character first name</param>
        /// <param name="lastName">Character last name</param>
        public async void SubmitName(string firstName, string lastName)
        {
            if (firstName != "" || lastName != "")
            {
                //gets the url from the data input
                string rankingsUrl = Library.characterparse(firstName, lastName, ServerSelected, RegionSelected);

                //puts the parse data into a list of Rankings class
                CharacterParses = await Library._download_serialized_json_data<List<Rankings>>(rankingsUrl);
            }
        }

        public string Test { get; set; }


        public ICommand GetParse { get; set; }

        public TimeLineBaseViewModel tlbv;

        public void ShowParse()
        {
            ActivateItem(tlbv = new TimeLineBaseViewModel(CharacterParses[this.SelectedParse]));
            tlbv.AddCharacterParseTimeline();
            this.Test = "Success"+ this.SelectedParse;
        }

        #endregion

    }
}
