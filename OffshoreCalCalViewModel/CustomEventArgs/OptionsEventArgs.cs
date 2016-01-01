using System;

namespace OffshoreCalCalViewModel.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs object that represents the 'options' dialog box values.
    /// TODO: Create and use an 'OffshoreOptions' object instead.
    /// </summary>
    public class OptionsEventArgs : EventArgs
    {
        public DateTime InitialDate { get; set; }
    }
}
