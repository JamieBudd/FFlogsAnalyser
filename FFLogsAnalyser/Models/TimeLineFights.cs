using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class TimeLineFights
    {
        public TimeLineFights(string fights, int fightsID, string colour)
        {
            this.Fights = fights;
            this.FightsID = fightsID;
            this.IsEnabled = true;
            this.Colour = colour;
        }

        #region Public Members
        
        public string Fights { get; set; }
        public int FightsID { get; set; }
        public bool IsEnabled { get; set; }
        public string Colour { get; set; }
        
        #endregion
    }
}
