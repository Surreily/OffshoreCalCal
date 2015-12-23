using OffshoreCalCal.Common;
using OffshoreCalCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OffshoreCalCal.ViewModels
{
    /// <summary>
    /// A view model for the options window.
    /// </summary>
    public class OptionsWindowViewModel : NotifyPropertyChangedBase
    {
        #region Constructor

        /// <summary>
        /// Constructs an OptionsWindowViewModel with an OffshoreData object.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public OptionsWindowViewModel(DateTime initialDate)
        {
            InitialDate = initialDate;
        }

        #endregion // Constructor

        #region Properties

        public DateTime InitialDate { get; set; }

        #region New starting date

        private DateTime _newStartingDate;
        public DateTime NewStartingDate
        {
            get
            {
                return _newStartingDate;
            }
            set
            {
                _newStartingDate = value;
                NotifyPropertyChanged("NewStartingDate");
            }
        }

        #endregion // New starting date

        #endregion

    }
}
