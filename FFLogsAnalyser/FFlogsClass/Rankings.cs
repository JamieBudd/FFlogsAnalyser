using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace FFLogsAnalyser
{
    public class Rankings
    {
        private int EncounterID { get; set; }
        public string EncounterName { get; set; }
        private string Class { get; set; }
        private string Spec { get; set; }
        private long Rank { get; set; }
        private long OutOf { get; set; }
        private double Duration { get; set; }
        private double StartTime { get; set; }
        public string ReportID { get; set; }
        public double FightID { get; set; }
        private long Difficulty { get; set; }
        private long CharacterID { get; set; }
        public string CharacterName { get; set; }
        private string Server { get; set; }
        public long Percentile { get; set; }
        private double IlvlKeyOrPatch { get; set; }
        public double Total { get; set; }
        private bool Estimated { get; set; }
        private string Message { get; set; }
    }
}