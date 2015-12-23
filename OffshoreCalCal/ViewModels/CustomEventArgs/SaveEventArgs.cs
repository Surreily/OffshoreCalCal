using OffshoreCalCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.ViewModels.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs class that is used when saving data.
    /// </summary>
    public class SaveEventArgs
    {
        public OffshoreData OffshoreData { get; set; }
    }
}
