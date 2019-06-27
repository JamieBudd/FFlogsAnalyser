using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class AddParseEvent
    {
        public AddParseEvent(int fightID, string reportID)
        {
            FightID = fightID;
            ReportID = reportID;
        }

        public int FightID;
        public string ReportID;
    }
}
