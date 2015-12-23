using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.ViewModels.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that represents the 'options' dialog box values.
    /// </summary>
    public class OptionsEventArgs : EventArgs
    {
        public DateTime InitialDate { get; set; }
    }
}
