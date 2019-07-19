using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class DeleteTimeLineEvent
    {

        #region Constructor
        
        public DeleteTimeLineEvent(int fightID)
        {
            this.FightID = fightID;
        }

        #endregion

        #region Public Members

        public int FightID { get; set; }
        
        #endregion
        
    }
}
