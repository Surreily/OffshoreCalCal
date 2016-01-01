using OffshoreCalCalCommon;
using OffshoreCalCalModel.Misc;
using OffshoreCalCalModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCalViewModel.ViewModels
{
    /// <summary>
    /// A view model that represents a day, and is used for the 'view day' window.
    /// </summary>
    public class ViewDayWindowViewModel : NotifyPropertyChangedBase
    {
        #region Constructor

        /// <summary>
        /// Constructor for this object.
        /// </summary>
        /// <param name="day"></param>
        public ViewDayWindowViewModel(OffshoreDay day)
        {
            _date = day.Date.ToString("dd-MMM-yyyy");
            _weekDay = day.Date.DayOfWeek.ToString();
            _dayType = DayTypeFormatter.ConvertToString(day.DayType);
            _location = day.Location;
            _description = day.Description;
        }

        #endregion // Constructor

        #region Properties

        #region Date

        private string _date;
        public string Date
        {
            get
            {
                return _date;
            }
        }

        #endregion // Date

        #region WeekDay

        private string _weekDay;
        public string WeekDay
        {
            get
            {
                return _weekDay;
            }
        }

        #endregion // WeekDay

        #region DayType

        private string _dayType;
        public string DayType
        {
            get
            {
                return _dayType;
            }
        }

        #endregion // DayType

        #region Location

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
        }

        #endregion // Location

        #region Description

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
        }

        #endregion // Description

        #endregion // Properties
    }
}
