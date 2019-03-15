using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser.FFlogsClass
{
    public class ReportFightID
    {
        public List<Fight> fights { get; set; }
        public string lang { get; set; }
        public List<Friendly> friendlies { get; set; }
        public List<Enemy> enemies { get; set; }
        public List<FriendlyPet> friendlyPets { get; set; }
        public List<object> enemyPets { get; set; }
        public List<object> phases { get; set; }
        public string title { get; set; }
        public string owner { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public int zone { get; set; }
    }

    public class Fight
    {
        public int id { get; set; }
        public int start_time { get; set; }
        public int end_time { get; set; }
        public int boss { get; set; }
        public int size { get; set; }
        public int difficulty { get; set; }
        public bool kill { get; set; }
        public int partial { get; set; }
        public bool standardComposition { get; set; }
        public int bossPercentage { get; set; }
        public int fightPercentage { get; set; }
        public int lastPhaseForPercentageDisplay { get; set; }
        public string name { get; set; }
        public int zoneID { get; set; }
        public string zoneName { get; set; }
    }

    public class Fight2
    {
        public int id { get; set; }
    }

    public class Friendly
    {
        public string name { get; set; }
        public int id { get; set; }
        public int guid { get; set; }
        public string type { get; set; }
        public List<Fight2> fights { get; set; }
    }

    public class Fight3
    {
        public int id { get; set; }
        public int instances { get; set; }
        public int? groups { get; set; }
    }

    public class Enemy
    {
        public string name { get; set; }
        public int id { get; set; }
        public int guid { get; set; }
        public string type { get; set; }
        public List<Fight3> fights { get; set; }
    }

    public class Fight4
    {
        public int id { get; set; }
        public int instances { get; set; }
    }

    public class FriendlyPet
    {
        public string name { get; set; }
        public int id { get; set; }
        public int guid { get; set; }
        public string type { get; set; }
        public int petOwner { get; set; }
        public List<Fight4> fights { get; set; }
    }
}
