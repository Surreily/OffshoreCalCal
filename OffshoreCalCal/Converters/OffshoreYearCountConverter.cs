using OffshoreCalCal.Enums;
using OffshoreCalCal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OffshoreCalCal.Converters
{
    /// <summary>
    /// This converter takes an OffshoreYearCount object along with a string parameter, and returns a 
    /// string representation of the corresponding value. ConvertBack is not supported.
    /// </summary>
    public class OffshoreYearCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                OffshoreYearCount counter = value as OffshoreYearCount;
                string param = parameter as string;

                if (counter != null && param != null)
                {
                    // Convert the parameter to a key
                    DayType key = DayTypeConverter.ConvertToDayType(param);

                    // Check if the key exists, and if so, return it
                    if (counter.Counter.ContainsKey(key)) return counter.Counter[key].ToString();
                }
            }

            // If something went wrong, or the day doesn't yet exist this year
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
