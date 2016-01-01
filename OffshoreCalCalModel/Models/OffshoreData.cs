using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCalModel.Models
{
    /// <summary>
    /// This class contains the main values of the data, and represents the dataset as a whole.
    /// </summary>
    public class OffshoreData
    {
        #region Constructor

        /// <summary>
        /// Constructs an instance of this class.
        /// </summary>
        public OffshoreData()
        {
            Days = new List<OffshoreDay>();
            YearCounts = new List<OffshoreYearCount>();
        }

        #endregion // Constructor

        #region Properties

        // Options
        public DateTime InitialDate { get; set; }

        // Previous locations
        public List<string> Locations { get; set; }

        // Current data
        public double Vacations { get; set; }
        public double DaysOff { get; set; }

        // List of days
        public IList<OffshoreDay> Days { get; set; }

        // List of year counts
        public IList<OffshoreYearCount> YearCounts { get; set; }

        #endregion // Properties
    }
}
