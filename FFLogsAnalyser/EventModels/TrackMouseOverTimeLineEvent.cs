using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class TrackMouseOverTimeLineEvent
    {
        public TrackMouseOverTimeLineEvent(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X;
        public double Y;
    }
}
