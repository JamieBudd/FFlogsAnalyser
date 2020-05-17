using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{

    [AddINotifyPropertyChangedInterface]

    public class ItemMenuPersonalBuffs : INotifyPropertyChanged
    {
        #region Constructor
        public ItemMenuPersonalBuffs(int buffname, string buffnamestring)
        {
            BuffName = buffname;
            IsChecked = true;
            BuffNameForMenu = buffnamestring.Replace("_", " ");
        }

        #endregion

        #region Public Members

        public event PropertyChangedEventHandler PropertyChanged;

        
        /// <summary>
        /// The name of the buff without underscores
        /// </summary>
        public string BuffNameForMenu { get; set; }

        /// <summary>
        /// If it is selected in the menu item
        /// </summary>
        public bool IsChecked { get; set; }
        
        /// <summary>
        /// Name of the Buff
        /// </summary>
        public int BuffName { get; set; }
        #endregion
    }
}
