using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    public class AddPersonalBuffsEvent
    {

        #region Constructor

        public AddPersonalBuffsEvent(FullyObservableCollection<ItemMenuPersonalBuffs> personalbuffname)
        {
            PersonalBuffName = personalbuffname;
        }

        #endregion

        #region Public Members

        public FullyObservableCollection<ItemMenuPersonalBuffs> PersonalBuffName;

        #endregion
    }
}
