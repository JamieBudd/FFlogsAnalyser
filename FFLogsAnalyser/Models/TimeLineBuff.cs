using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.Models
{
    public class TimeLineBuff
    {
        public string Name { get; set; }
        public List<Instance> instance = new List<Instance>();

        //public List<TargetID> target;

        //public class TargetID
        //{
        //    public int ID;
        //    public List<Instance> instance;
        //}

        public class Instance
        {
            public int SourceID;
            public double StartTime;
            public double EndTime;
            public bool complete = false;
        }
    }
}
