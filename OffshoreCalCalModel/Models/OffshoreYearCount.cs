using OffshoreCalCalModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCalModel.Models
{
    /// <summary>
    /// Counts the number of days for each year.
    /// </summary>
    public class OffshoreYearCount
    {
        #region Constructor

        /// <summary>
        /// Constructs an instance of this class.
        /// </summary>
        public OffshoreYearCount()
        {
            Counter = new Dictionary<DayType, int>();
        }

        #endregion // Constructor

        #region Properties

        public int Year { get; set; }
        public IDictionary<DayType, int> Counter { get; set; }

        #endregion // Properties
    }
}
