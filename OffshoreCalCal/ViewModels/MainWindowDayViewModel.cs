using OffshoreCalCal.Common;
using OffshoreCalCal.Converters;
using OffshoreCalCal.Enums;
using OffshoreCalCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.ViewModels
{
    public class MainWindowDayViewModel : NotifyPropertyChangedBase
    {
        #region Constructor

        /// <summary>
        /// Constructs an instance of this class. This class represents a day in the main window's list.
        /// </summary>
        /// <param name="offshoreDay">The OffshoreDay to represent.</param>
        public MainWindowDayViewModel(int index, OffshoreDay offshoreDay)
        {
            _index = index;
            _offshoreDay = offshoreDay;
            CalculateWeekend();
        }

        #endregion // Constructor

        #region Properties

        #region Model

        private OffshoreDay _offshoreDay;

        #endregion // Model   

        #region Index
        private int _index;
        public int Index
        {
            get
            {
                return _index;
            }
        }

        #endregion // Index

        #region Date

        public DateTime Date
        {
            get
            {
                return _offshoreDay.Date;
            }
            set
            {
                _offshoreDay.Date = value;
                if (CalculateWeekend()) NotifyPropertyChanged("DayTypeString");
                NotifyPropertyChanged("DateString");
            }
        }

        public string DateString
        {
            get { return _offshoreDay.Date.ToString("dd-MMM-yyyy"); }
        }

        #endregion // Date

        #region WeekDay

        public string WeekDay
        {
            get
            {
                return _offshoreDay.Date.DayOfWeek.ToString();
            }
        }

        #endregion // WeekDay

        #region DayType

        // DayType
        public DayType DayType
        {
            get
            {
                return _offshoreDay.DayType;
            }
            set
            {
                _offshoreDay.DayType = value;
                CalculateWeekend();
                NotifyPropertyChanged("DayTypeString");
            }
        }

        // Formats the day type as a string, and appends the location if one exists.
        public string DayTypeString
        {
            get
            {
                return DayTypeConverter.ConvertToString(_offshoreDay.DayType);
            }
        }

        #endregion // DayType

        #region Location

        public string Location
        {
            get
            {
                return _offshoreDay.Location;
            }
            set
            {
                _offshoreDay.Location = value;
                NotifyPropertyChanged("Location");
            }
        }

        #endregion // Location

        #region Description

        public string Description
        {
            get
            {
                return _offshoreDay.Description;
            }
            set
            {
                _offshoreDay.Description = value;
                NotifyPropertyChanged("Description");
                NotifyPropertyChanged("DescriptionNoReturns");
            }
        }

        // Returns the description without any newline characters.
        public string DescriptionNoReturns
        {
            get
            {
                return _offshoreDay.Description.Replace(System.Environment.NewLine, " ").Trim();
            }
        }

        #endregion // Description  

        #endregion // Properties

        #region Functions

        #region Weekend

        /// <summary>
        /// Calculates whether a DayOff or Vacation need to be converted to a Weekend. This method is triggered by 
        /// any changes to this object's date or day type.
        /// </summary>
        /// <returns>A boolean indicating whether a change was made.</returns>
        private bool CalculateWeekend()
        {
            if ((_offshoreDay.DayType == DayType.DayOff || _offshoreDay.DayType == DayType.Vacation) && (_offshoreDay.Date.DayOfWeek == DayOfWeek.Saturday || _offshoreDay.Date.DayOfWeek == DayOfWeek.Sunday))
            {
                _offshoreDay.DayType = DayType.Weekend;
                return true;
            }
            /*else if (_offshoreDay.DayType == DayType.Weekend && !(_offshoreDay.Date.DayOfWeek == DayOfWeek.Saturday || _offshoreDay.Date.DayOfWeek == DayOfWeek.Sunday))
            {
                _offshoreDay.DayType = DayType.DayOff;
                return true;
            }*/
            return false;
        }

        #endregion // Weekend

        #endregion // Functions
    }
}
