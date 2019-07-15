using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class AddParseEvent
    {
        #region Constructor

        public AddParseEvent(int fightID, string reportID)
        {
            FightID = fightID;
            ReportID = reportID;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// ID of the fight
        /// </summary>
        public int FightID;

        /// <summary>
        /// ID of the report
        /// </summary>
        public string ReportID;

        #endregion
    }
}
