using OffshoreCalCal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.Converters
{
    /// <summary>
    /// Converts DayType values to and from strings.
    /// </summary>
    class DayTypeConverter
    {
        /// <summary>
        /// Converts from a DayType to a string.
        /// </summary>
        /// <param name="value">The DayType to convert from.</param>
        /// <returns>The converted string.</returns>
        public static string ConvertToString(DayType value)
        {
            switch(value)
            {
                case DayType.Offshore: return "Offshore";
                case DayType.OffshoreBh: return "Offshore (BH)";
                case DayType.DayOff: return "Day Off";
                case DayType.DayOffBh: return "Day Off (BH)";
                case DayType.Weekend: return "Weekend";
                case DayType.Sick: return "Sick";
                case DayType.Vacation: return "Vacation";
                case DayType.Base: return "Base";
                case DayType.CompLeave: return "Comp. Leave";
                case DayType.Training: return "Training";
                default: throw new ArgumentException("The specified DayType value is not handled in this function.");
            }
        }

        /// <summary>
        /// Converts from a string to a DayType.
        /// </summary>
        /// <param name="value">The string to convert from.</param>
        /// <returns>The converted DayType.</returns>
        public static DayType ConvertToDayType(string value)
        {
            switch(value)
            {
                case "Offshore": return DayType.Offshore;
                case "Offshore (BH)": return DayType.OffshoreBh;
                case "Day Off": return DayType.DayOff;
                case "Day Off (BH)": return DayType.DayOffBh;
                case "Weekend": return DayType.Weekend;
                case "Sick": return DayType.Sick;
                case "Vacation": return DayType.Vacation;
                case "Base": return DayType.Base;
                case "Comp. Leave": return DayType.CompLeave;
                case "Training": return DayType.Training;
                default: throw new ArgumentException("A DayType value matching the specified name does not exist.");
            }
        }
    }
}
