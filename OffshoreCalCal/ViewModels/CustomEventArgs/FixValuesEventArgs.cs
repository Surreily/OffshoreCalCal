using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.ViewModels.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that contains values required for the 'fix inaccurate values' window.
    /// </summary>
    public class FixValuesEventArgs : EventArgs
    {
        public double Vacations { get; set; }
        public double DaysOff { get; set; }
    }
}
