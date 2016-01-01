using OffshoreCalCalModel.Misc;
using OffshoreCalCalModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OffshoreCalCalStorage
{

    /// <summary>
    /// Handles the saving of a file, and conversion to XML.
    /// </summary>
    public class DataSaver
    {
        // File location
        private string _location;

        // The data to save
        private OffshoreData _data;
        
        /// <summary>
        /// Constructs an instance of the DataSaver.
        /// </summary>
        /// <param name="location">The location of the XML file to save to.</param>
        /// <param name="data">The data to save.</param>
        public DataSaver(string location, OffshoreData data)
        {
            _location = location;
            _data = data;
        }

        /// <summary>
        /// Saves the data to an XML file.
        /// </summary>
        public void Save()
        {
            try
            {
                // Create the XmlDocument object
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.DocumentElement;

                // Write the XML declaration
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.InsertBefore(declaration, root);

                // Add the OffshoreCalCal element
                XmlElement offshoreCalCal = doc.CreateElement(string.Empty, "OffshoreCalCal", string.Empty);
                doc.AppendChild(offshoreCalCal);

                // Add the Options element along with its data
                XmlElement options = doc.CreateElement(string.Empty, "Options", string.Empty);
                offshoreCalCal.AppendChild(options);

                XmlElement initialDate = doc.CreateElement(string.Empty, "InitialDate", string.Empty);
                options.AppendChild(initialDate);

                XmlText initialDateText = doc.CreateTextNode(Convert.ToString(_data.InitialDate));
                initialDate.AppendChild(initialDateText);

                // Add the CurrentData element along with its data
                XmlElement currentData = doc.CreateElement(string.Empty, "CurrentData", string.Empty);
                offshoreCalCal.AppendChild(currentData);

                XmlElement vacations = doc.CreateElement(string.Empty, "Vacations", string.Empty);
                currentData.AppendChild(vacations);

                XmlText vacationsText = doc.CreateTextNode(Convert.ToString(_data.Vacations));
                vacations.AppendChild(vacationsText);

                XmlElement daysOff = doc.CreateElement(string.Empty, "DaysOff", string.Empty);
                currentData.AppendChild(daysOff);

                XmlText daysOffText = doc.CreateTextNode(Convert.ToString(_data.DaysOff));
                daysOff.AppendChild(daysOffText);

                // Add each day
                XmlElement days = doc.CreateElement(string.Empty, "Days", string.Empty);
                offshoreCalCal.AppendChild(days);

                foreach (var offshoreDay in _data.Days)
                {
                    // Create the Day element
                    XmlElement day = doc.CreateElement(string.Empty, "Day", string.Empty);
                    days.AppendChild(day);

                    // Save type
                    XmlElement type = doc.CreateElement(string.Empty, "Type", string.Empty);
                    day.AppendChild(type);

                    XmlText typeText = doc.CreateTextNode(DayTypeFormatter.ConvertToString(offshoreDay.DayType));
                    type.AppendChild(typeText);

                    // Save description
                    if (!offshoreDay.Description.Equals(string.Empty))
                    {
                        XmlElement desc = doc.CreateElement(string.Empty, "Desc", string.Empty);
                        day.AppendChild(desc);

                        XmlText descText = doc.CreateTextNode(offshoreDay.Description);
                        desc.AppendChild(descText);
                    }
                    
                    // Save location
                    if (!offshoreDay.Location.Equals(string.Empty))
                    {
                        XmlElement loc = doc.CreateElement(string.Empty, "Loc", string.Empty);
                        day.AppendChild(loc);

                        XmlText locText = doc.CreateTextNode(offshoreDay.Location);
                        loc.AppendChild(locText);
                    }
                }

                // Save the document
                doc.Save(_location);
            }
            catch
            {
                throw;
            }
        }
    }
}
