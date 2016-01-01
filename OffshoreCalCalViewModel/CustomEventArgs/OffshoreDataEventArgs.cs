using OffshoreCalCalViewModel.ViewModels;
using System;

namespace OffshoreCalCalViewModel.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that contains a pointer to the MainWindowViewModel object.
    /// </summary>
    public class OffshoreDataEventArgs : EventArgs
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
    }
}
