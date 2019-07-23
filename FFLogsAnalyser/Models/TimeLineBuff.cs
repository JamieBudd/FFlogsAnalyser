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
        public int TimeLineGroupIndex { get; set; }
        public string Colour { get; set; }
        public string BuffColour
        {
            get
            {
                string BuffName = Name.Replace(' ', '_');
                int BuffNumber = (int)Enum.Parse(typeof(Buffs), BuffName);
                Enum Colour = (BuffColours)BuffNumber;
                return Colour.ToString();
            }
        }

        

        public class Instance
        {
            public int SourceID;
            public double StartTime;
            public double EndTime;
            public bool complete = false;
        }
    }
}
