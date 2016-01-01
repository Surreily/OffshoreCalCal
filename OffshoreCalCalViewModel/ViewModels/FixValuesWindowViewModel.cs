using OffshoreCalCalCommon;
using System;

namespace OffshoreCalCalViewModel.ViewModels
{
    /// <summary>
    /// A view model for the 'Fix Inaccurate Values' view.
    /// </summary>
    public class FixValuesWindowViewModel : NotifyPropertyChangedBase
    {
        #region Constructor

        /// <summary>
        /// Constructs an instance of this object.
        /// </summary>
        /// <param name="daysOff">The days off.</param>
        /// <param name="vacations">The vacations.</param>
        public FixValuesWindowViewModel(double daysOff, double vacations)
        {
            _daysOff = Convert.ToString(Math.Round(daysOff, 3, MidpointRounding.AwayFromZero));
            _vacations = Convert.ToString(Math.Round(vacations, 3, MidpointRounding.AwayFromZero));
        }

        #endregion // Constructor

        #region Properties

        #region DaysOff

        private string _daysOff;
        public string DaysOff
        {
            get
            {
                return _daysOff;
            }
            set
            {
                _daysOff = value;
                NotifyPropertyChanged("DaysOff");
                NotifyPropertyChanged("BtnEnabled");
            }
        }

        private string _txtDaysOff;
        public string TxtDaysOff
        {
            get
            {
                return _txtDaysOff;
            }
            set
            {
                _txtDaysOff = value;
            }
        }

        #endregion // DaysOff

        #region Vacations

        private string _vacations;
        public string Vacations
        {
            get
            {
                return _vacations;
            }
            set
            {
                _vacations = value;
                NotifyPropertyChanged("Vacations");
                NotifyPropertyChanged("BtnEnabled");
            }
        }

        #endregion // Vacations

        #region BtnEnabled

        public bool BtnEnabled
        {
            get
            {
                double value;
                return (double.TryParse(_daysOff, out value)) && (double.TryParse(_vacations, out value));
            }
        }

        #endregion // BtnEnabled

        #endregion // Properties

    }
}
