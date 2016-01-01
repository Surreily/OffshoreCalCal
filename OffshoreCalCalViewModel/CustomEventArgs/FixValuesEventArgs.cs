using System;

namespace OffshoreCalCalViewModel.CustomEventArgs
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
