using System;

namespace OffshoreCalCalViewModel.CustomEventArgs
{
    /// <summary>
    /// Represents a yes/no choice in a dialog box.
    /// </summary>
    public class DialogEventArgs : EventArgs
    {
        #region IsAccepted

        private bool _isAccepted = false;
        public bool IsAccepted
        {
            get
            {
                return _isAccepted;
            }
            set
            {
                _isAccepted = value;
            }
        }

        #endregion // IsAccepted
    }
}
