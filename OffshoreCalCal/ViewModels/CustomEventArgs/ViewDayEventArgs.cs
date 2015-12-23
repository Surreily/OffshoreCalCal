using OffshoreCalCal.Enums;
using OffshoreCalCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.ViewModels.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that is used to pass information to the 'View Day' dialog box.
    /// </summary>
    public class ViewDayEventArgs : EventArgs
    {
        public OffshoreDay Day { get; set; }
    }
}
