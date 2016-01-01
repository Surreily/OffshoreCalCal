using OffshoreCalCalCommon;
using System.ComponentModel;
using System.Windows.Input;

namespace OffshoreCalCalViewModel.ViewModels
{
    /// <summary>
    /// A view model for the 'About' view.
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
