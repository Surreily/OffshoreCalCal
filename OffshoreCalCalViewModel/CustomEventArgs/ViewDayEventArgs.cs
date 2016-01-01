using OffshoreCalCalModel.Models;
using System;

namespace OffshoreCalCalViewModel.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that passes an OffshoreDay object when the
    /// viewing of a single day is required.
    /// </summary>
    public class ViewDayEventArgs : EventArgs
    {
        public OffshoreDay Day { get; set; }
    }
}
