using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.FFlogsClass
{
    class ReportEvent
    {
        public List<Event> events { get; set; }
        public int nextPageTimestamp { get; set; }
    }
    public class Ability
    {
        public string name { get; set; }
        public int guid { get; set; }
        public int type { get; set; }
        public string abilityIcon { get; set; }
    }

    public class SourceResources
    {
        public int hitPoints { get; set; }
        public int maxHitPoints { get; set; }
        public int mp { get; set; }
        public int maxMP { get; set; }
        public int tp { get; set; }
        public int maxTP { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class TargetResources
    {
        public int hitPoints { get; set; }
        public int maxHitPoints { get; set; }
        public int mp { get; set; }
        public int maxMP { get; set; }
        public int tp { get; set; }
        public int maxTP { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Target
    {
        public string name { get; set; }
        public int id { get; set; }
        public int guid { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
    }

    public class Event
    {
        public int timestamp { get; set; }
        public string type { get; set; }
        public int sourceID { get; set; }
        public bool sourceIsFriendly { get; set; }
        public int targetID { get; set; }
        public int targetInstance { get; set; }
        public bool targetIsFriendly { get; set; }
        public Ability ability { get; set; }
        public int hitType { get; set; }
        public int amount { get; set; }
        public int absorbed { get; set; }
        public double debugMultiplier { get; set; }
        public SourceResources sourceResources { get; set; }
        public TargetResources targetResources { get; set; }
        public int? stack { get; set; }
        public bool? multistrike { get; set; }
        public int? sourceInstance { get; set; }
        public int? overheal { get; set; }
        public bool? tick { get; set; }
        public double? finalizedAmount { get; set; }
        public bool? simulated { get; set; }
        public double? expectedAmount { get; set; }
        public double? expectedCritRate { get; set; }
        public double? actorPotencyRatio { get; set; }
        public double? guessAmount { get; set; }
        public double? multiplier { get; set; }
        public double? directHitPercentage { get; set; }
        public Target target { get; set; }
        public int? absorb { get; set; }
    }
}
