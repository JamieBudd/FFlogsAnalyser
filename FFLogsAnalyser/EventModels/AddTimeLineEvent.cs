using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class AddTimeLineEvent
    {
        #region Constructor

        public AddTimeLineEvent(string timeLineFightCode, int timeLineID, string colour)
        {
            TimeLineFightCode = timeLineFightCode;
            TimeLineID = timeLineID;
            Colour = colour;
        }

        #endregion

        #region Public Members

        public string TimeLineFightCode;
        public int TimeLineID;
        public string Colour;

        #endregion
    }
}
