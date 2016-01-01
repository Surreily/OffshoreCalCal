using OffshoreCalCalModel.Enums;
using OffshoreCalCalModel.Misc;
using OffshoreCalCalModel.Models;
using System;
using System.Linq;
using System.Xml;

namespace OffshoreCalCalStorage
{
    /// <summary>
    /// Handles the loading of data from a file, and converting it from XML.
    /// </summary>
    public class DataLoader
    {
        // File location
        private string _location;

        /// <summary>
        /// Constructs an instance of the DataLoader.
        /// </summary>
        /// <param name="location">The location of the XML file to load.</param>
        public DataLoader(string location)
        {
            _location = location;
        }

        /// <summary>
        /// Load the XML file, and convert it to an OffshoreData object.
        /// </summary>
        /// <returns>An OffshoreData object based on the contents of the XML.</returns>
        public OffshoreData Load()
        {
            try
            {
                // Load the document
                XmlDocument doc = new XmlDocument();
                doc.Load(_location);

                // Construct an OffshoreData object
                OffshoreData offshoreData = new OffshoreData();

                // Set options (and store runningDate for later use)
                XmlNode options = doc.SelectSingleNode("/OffshoreCalCal/Options");

                string initialDateString = options.SelectSingleNode("InitialDate").InnerText;
                IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                DateTime runningDate = Convert.ToDateTime(initialDateString, culture);
                offshoreData.InitialDate = runningDate;

                // Set current data
                XmlNode currentData = doc.SelectSingleNode("/OffshoreCalCal/CurrentData");

                string vacations = currentData.SelectSingleNode("Vacations").InnerText;
                offshoreData.Vacations = Convert.ToDouble(vacations);

                string daysOff = currentData.SelectSingleNode("DaysOff").InnerText;
                offshoreData.DaysOff = Convert.ToDouble(daysOff);

                // Set each day
                XmlNodeList days = doc.SelectSingleNode("/OffshoreCalCal/Days").SelectNodes("Day");
                foreach (XmlNode day in days)
                {
                    // Create the OffshoreDay object and populate it with data
                    OffshoreDay offshoreDay = new OffshoreDay();

                    // Load description
                    if (day.SelectSingleNode("Desc") != null)
                    {
                        offshoreDay.Description = day.SelectSingleNode("Desc").InnerText;
                    }
                    else
                    {
                        offshoreDay.Description = string.Empty;
                    }

                    // Load location
                    if (day.SelectSingleNode("Loc") != null)
                    {
                        string location = day.SelectSingleNode("Loc").InnerText;
                        offshoreDay.Location = location;
                    }
                    else
                    {
                        offshoreDay.Location = string.Empty;
                    }

                    // Load day type (and keep a variable for reference later)
                    DayType dayType = DayTypeFormatter.ConvertToDayType(day.SelectSingleNode("Type").InnerText);
                    offshoreDay.DayType = dayType;

                    // Determine the date
                    offshoreDay.Date = runningDate;

                    // Add the OffshoreDay to the data
                    offshoreData.Days.Add(offshoreDay);

                    // If this is the first day of a year, create a new YearCount object
                    if (offshoreData.YearCounts.Count == 0 || runningDate.DayOfYear == 1) offshoreData.YearCounts.Add(new OffshoreYearCount() { Year = runningDate.Year });

                    // If the day type to be added does not yet exist, create it, otherwise just increment it
                    if (!offshoreData.YearCounts.Last().Counter.ContainsKey(dayType))
                    {
                        offshoreData.YearCounts.Last().Counter.Add(dayType, 1);
                    } 
                    else
                    {
                        offshoreData.YearCounts.Last().Counter[dayType] += 1;
                    }

                    // Increment the running date
                    runningDate = runningDate.AddDays(1);
                }

                // Return the completed object
                return offshoreData;
            }
            catch
            {
                throw;
            }
        }
    }
}
