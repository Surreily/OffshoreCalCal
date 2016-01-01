using OffshoreCalCalModel.Models;
using System;
using System.Collections.Generic;

namespace OffshoreCalCalViewModel.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that represents the 'edit day' dialog box values.
    /// </summary>
    public class EditDayEventArgs : EventArgs
    {
        public int Index { get; set; }
        public OffshoreDay Day { get; set; }
        public List<string> Locations { get; set; }
    }
}
