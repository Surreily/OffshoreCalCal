using OffshoreCalCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.ViewModels.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that contains a pointer to the OffshoreData object.
    /// </summary>
    public class OffshoreDataEventArgs : EventArgs
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
    }
}
