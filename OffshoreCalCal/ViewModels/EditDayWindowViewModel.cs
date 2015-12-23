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
    /// <summary>
    /// A view model for the edit day window.
    /// </summary>
    public class EditDayWindowViewModel : NotifyPropertyChangedBase
    {
        #region Constructor

        public EditDayWindowViewModel(int index, OffshoreDay day, List<string> locations)
        {
            Index = index;
            _date = day.Date;
            _dayType = (day.DayType == DayType.Weekend ? DayType.DayOff : day.DayType);
            _description = day.Description;
            _txtLocation = day.Location;
            _locations = locations;

            DayTypeStrings = new List<string>();
            var values = Enum.GetValues(typeof(DayType));
            foreach (DayType value in values)
            {
                if (value != DayType.Weekend)
                {
                    DayTypeStrings.Add(DayTypeConverter.ConvertToString(value));
                }
            }
        }

        #endregion // Constructor

        #region Properties

        #region Index

        public int Index { get; set; }

        #endregion // Index

        #region Date

        private DateTime _date;
        public string DateString {
            get
            {
                return _date.ToString("dd-MMM-yyyy");
            }
        }

        #endregion // Date

        #region Day type

        private DayType _dayType;
        public DayType DayType
        {
            get
            {
                return _dayType;
            }
            set
            {
                _dayType = value;
            }
        }

        public string DayTypeString
        {
            get
            {
                return DayTypeConverter.ConvertToString(_dayType);
            }
            set
            {
                _dayType = DayTypeConverter.ConvertToDayType(value);
                NotifyPropertyChanged("DayTypeString");
            }
        }

        #endregion // Day type

        #region Description

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        #endregion // Description

        #region TxtLocation

        private string _txtLocation;
        public string TxtLocation
        {
            get
            {
                return _txtLocation;
            }
            set
            {
                _txtLocation = value;
                NotifyPropertyChanged("TxtLocation");
            }
        }

        #endregion // TxtLocation

        #region Locations

        private List<string> _locations;
        public List<string> Locations
        {
            get
            {
                return _locations;
            }
        }

        #endregion // Locations

        #region UI bindings

        public bool UpdateValues { get; set; }

        public List<string> DayTypeStrings { get; }

        #endregion // UI bindings

        #endregion // Properties
    }
}
