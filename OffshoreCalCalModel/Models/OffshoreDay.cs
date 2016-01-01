using OffshoreCalCalModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCalModel.Models
{
    /// <summary>
    /// This class represents a day.
    /// </summary>
    public class OffshoreDay
    {
        #region Properties

        // The location of the day.
        public string Location { get; set; }

        // A description of the day.
        public string Description { get; set; }

        // The type of day.
        public DayType DayType { get; set; }

        // The date of the day
        public DateTime Date { get; set; }

        #endregion // Properties
    }
}
