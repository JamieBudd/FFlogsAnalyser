using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class TrackMouseOverTimeLineEvent
    {
        #region Constructor

        public TrackMouseOverTimeLineEvent(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// X point of the mouse over the timeline
        /// </summary>
        public double X;

        /// <summary>
        /// Y point of the mouse over the timeline
        /// </summary>
        public double Y;

        #endregion
    }
}
