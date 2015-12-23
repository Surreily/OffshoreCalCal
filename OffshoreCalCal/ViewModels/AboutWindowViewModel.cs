using OffshoreCalCal.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OffshoreCalCal.ViewModels
{
    /// <summary>
    /// A view model for the about window.
    /// </summary>
    public class AboutWindowViewModel
    {
        #region Commands

        #region Close

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => this.CloseWindow());
                }
                return _closeCommand;
            }
        }

        private void CloseWindow()
        {

            // Close this window
            
        }

        #endregion // Close

        #endregion // Commands

        #region Event handlers

        public void CloseEventHandler(object sender, CancelEventArgs e)
        {
            CloseWindow();
        }

        #endregion // Event handlers
    }
}
