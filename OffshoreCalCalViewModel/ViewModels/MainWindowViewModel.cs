using OffshoreCalCalCommon;
using OffshoreCalCalModel.Enums;
using OffshoreCalCalModel.Misc;
using OffshoreCalCalModel.Models;
using OffshoreCalCalStorage;
using OffshoreCalCalViewModel.CustomEventArgs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace OffshoreCalCalViewModel.ViewModels
{
    /// <summary>
    /// A view model for the 'main' view.
    /// </summary>
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        #region Constructor

        public MainWindowViewModel(OffshoreData offshoreData)
        {
            // Check for a null OffshoreData object, and if so, initialize it to default values
            if (offshoreData == null)
            {
                _offshoreData = new OffshoreData() { DaysOff = 0, Vacations = 0, InitialDate = DateTime.Now };
            }
            else
            {
                _offshoreData = offshoreData;
            }

            // Initialize lists
            OffshoreDays = new ObservableCollection<MainWindowDayViewModel>();

            // Set the model
            foreach (OffshoreDay day in _offshoreData.Days)
            {
                OffshoreDays.Add(new MainWindowDayViewModel(OffshoreDays.Count, day));
            }

            // Set the previous five locations
            _locations = new ObservableCollection<string>();
            for (int i = _offshoreData.Days.Count - 1; i > 0; i--)
            {
                string potentialLocation = _offshoreData.Days[i].Location;
                if (potentialLocation != null && !potentialLocation.Equals(string.Empty) && !_locations.Contains(potentialLocation)) _locations.Add(potentialLocation);
                if (_locations.Count >= 5) break;
            }

            // Set default UI items
            _statusBarText = "Ready.";
            _changesMade = false;
        }

        #endregion // Constructor

        #region Properties

        #region Model

        private OffshoreData _offshoreData;

        #endregion

        #region View model

        public ObservableCollection<MainWindowDayViewModel> OffshoreDays { get; set; }

        #endregion

        #region Days off

        // The number of available days off
        public double DaysOff
        {
            get
            {
                return Math.Round(_offshoreData.DaysOff, 3, MidpointRounding.AwayFromZero);
            }
            set
            {
                _offshoreData.DaysOff = value;
                NotifyPropertyChanged("DaysOff");
            }
        }

        #endregion

        #region Vacations

        // The number of available vacations
        public double Vacations
        {
            get
            {
                return Math.Round(_offshoreData.Vacations, 3, MidpointRounding.AwayFromZero);
            }
            set
            {
                _offshoreData.Vacations = value;
                NotifyPropertyChanged("Vacations");
            }
        }

        #endregion

        #region Description text

        // Binding for the description text box
        private string _txtDescription;
        public string TxtDescription
        {
            get
            {
                return _txtDescription;
            }
            set
            {
                _txtDescription = value;
                NotifyPropertyChanged("TxtDescription");
            }
        }

        #endregion

        #region TxtLocation

        // Binding for the value of the location combo box
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

        // A list of the five previous locations for the combo box
        private ObservableCollection<string> _locations;
        public ObservableCollection<string> Locations
        {
            get
            {
                return _locations;
            }
        }

        #endregion // Locations

        #region Counter

        // A counter of the number of each type of day for the current year
        public OffshoreYearCount Counter
        {
            get
            {
                if (_offshoreData.YearCounts.Count > 0)
                {
                    return _offshoreData.YearCounts.Last();
                }
                return null;
            }
        }

        #endregion // Count

        #region Status bar text

        // The text shown in the status bar
        private string _statusBarText;
        public string StatusBarText
        {
            get
            {
                return _statusBarText;
            }
            set
            {
                _statusBarText = value;
                NotifyPropertyChanged("StatusBarText");
            }
        }

        #endregion

        #region Changes made

        private bool _changesMade;
        public bool ChangesMade
        {
            get
            {
                return _changesMade;
            }
            set
            {
                _changesMade = value;
                NotifyPropertyChanged("ChangesMade");
            }
        }

        #endregion // Changes made

        #endregion // Properties

        #region Commands

        #region Add day

        // This command adds a day. The parameter determines the type of day to add.
        private ICommand _addDayCommand;
        public ICommand AddDayCommand
        {
            get
            {
                if (_addDayCommand == null)
                {
                    _addDayCommand = new RelayCommand(param => this.AddDay((DayType)param));
                }
                return _addDayCommand;
            }
        }

        /// <summary>
        /// Adds a day, performing all required calculations.
        /// </summary>
        /// <param name="type">The type of day to add.</param>
        private void AddDay(DayType type)
        {
            // Determine the new date
            DateTime newDate = _offshoreData.InitialDate;
            if (_offshoreData.Days != null && _offshoreData.Days.Any())
            {
                newDate = _offshoreData.Days.Last().Date.AddDays(1);
            }

            // Determine the type of day
            DayType newType = type;

            // Create and add the day (as well as the view model)
            OffshoreDay offshoreDay = new OffshoreDay() { DayType = newType, Description = (TxtDescription ?? string.Empty), Location = (_txtLocation ?? string.Empty), Date = newDate };
            _offshoreData.Days.Add(offshoreDay);
            OffshoreDays.Add(new MainWindowDayViewModel(OffshoreDays.Count, offshoreDay));

            // If the location was provided, update the list to include it
            string newLocation = _txtLocation;
            if (newLocation != null && !newLocation.Equals(string.Empty)) UpdateLocations(newLocation);

            // Calculate changes to the statistics
            CalculateAddDay(offshoreDay.DayType, offshoreDay.Date);
            CountDay(offshoreDay.DayType, offshoreDay.Date);

            // Set status bar
            StatusBarText = "Added day " + DayTypeFormatter.ConvertToString(offshoreDay.DayType) + ".";
            ChangesMade = true;

            // Raise the NewDayAdded event
            OnNewDayAdded();

            // Notify changes
            NotifyPropertyChanged("Counter");
        }

        #endregion // Add day

        #region Remove day

        // This command removes a day.
        private ICommand _removeDayCommand;
        public ICommand RemoveDayCommand
        {
            get
            {
                if (_removeDayCommand == null)
                {
                    _removeDayCommand = new RelayCommand(param => this.RemoveDay());
                }
                return _removeDayCommand;
            }
        }

        /// <summary>
        /// Removes a day, performing calculations to reverse the effect of adding a day.
        /// </summary>
        private void RemoveDay()
        {
            // Check if there is a day to remove
            if (_offshoreData.Days.Count == 0) return;

            // Get the day to remove
            OffshoreDay day = _offshoreData.Days.Last();

            // Remove the day (as well as the view model)
            _offshoreData.Days.RemoveAt(_offshoreData.Days.Count - 1);
            OffshoreDays.RemoveAt(OffshoreDays.Count - 1);

            // Calculate changes to the statistics
            CalculateRemoveDay(day.DayType, day.Date);
            UncountDay(day.DayType, day.Date);

            // Set status bar
            StatusBarText = "Removed day " + DayTypeFormatter.ConvertToString(day.DayType) + ".";
            
            // Notify changes
            NotifyPropertyChanged("Counter");
        }

        #endregion // Remove day

        #region Save

        // This command saves data to permanent storage.
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => this.Save());
                }
                return _saveCommand;
            }
        }

        /// <summary>
        /// Saves the data to a file. The file is specified inside the function.
        /// </summary>
        private void Save()
        {
            // Set the status bar
            StatusBarText = "Saving file...";

            // Create a DataSaver object
            /*DataSaver saver = new DataSaver("C:\\Users\\Sean\\Desktop\\offshorecalcal.xml", _offshoreData);
            saver.Save();*/
            OnSaveCalled();

            // Set the status bar
            StatusBarText = "Saved.";
            _changesMade = false;
        }

        #endregion // Save

        #region Save Backup

        // This command saves a backup file with the current date as the filename.
        private ICommand _saveBackupCommand;
        public ICommand SaveBackupCommand
        {
            get
            {
                if (_saveBackupCommand == null)
                {
                    _saveBackupCommand = new RelayCommand(param => this.SaveBackup());
                }
                return _saveBackupCommand;
            }
        }

        /// <summary>
        /// Saves a backup copy of the data in the application's directory.
        /// </summary>
        private void SaveBackup()
        {
            // Set the status bar
            StatusBarText = "Saving backup...";

            DateTime now = DateTime.Now;
            string filename = "OffshoreCalCal_Backup_" + now.Day + "-" + now.Month + "-" + now.Year;
            DataSaver saver = new DataSaver(filename, _offshoreData);
            saver.Save();

            // Set the status bar
            StatusBarText = "Backup saved as \"" + filename + "\".";
        }

        #endregion // Save Backup

        #region About

        // This command displays the 'about' window.
        private ICommand _aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (_aboutCommand == null)
                {
                    _aboutCommand = new RelayCommand(param => this.ShowAboutWindow());
                }
                return _aboutCommand;
            }
        }

        /// <summary>
        /// Raises an event to show the 'about' window.
        /// </summary>
        private void ShowAboutWindow()
        {
            OnAboutRequested();
        }

        #endregion // About

        #region Options

        // This command shows the 'options' window.
        private ICommand _optionsCommand;
        public ICommand OptionsCommand
        {
            get
            {
                if (_optionsCommand == null)
                {
                    _optionsCommand = new RelayCommand(param => this.ShowOptionsWindow());
                }
                return _optionsCommand;
            }
        }

        /// <summary>
        /// Create and show the 'options' window.
        /// </summary>
        private void ShowOptionsWindow()
        {
            OnOptionsRequested();
        }

        #endregion // Options

        #region View day

        // This command shows the 'view day' window.
        private ICommand _viewDayCommand;
        public ICommand ViewDayCommand
        {
            get
            {
                if (_viewDayCommand == null)
                {
                    _viewDayCommand = new RelayCommand(param => this.ViewDay((int) param));
                }
                return _viewDayCommand;
            }
        }

        private void ViewDay(int index)
        {
            // Ensure something is selected
            if (index == -1)
            {
                StatusBarText = "To view a day, please select something first.";
                return;
            }

            OnViewDayRequested(index);
        }

        #endregion View day

        #region Edit day

        // This command shows the 'edit day' window.
        private ICommand _editDayCommand;
        public ICommand EditDayCommand
        {
            get
            {
                if (_editDayCommand == null)
                {
                    _editDayCommand = new RelayCommand(param => this.EditDay((int) param));
                }
                return _editDayCommand;
            }
        }

        /// <summary>
        /// Create and show the 'edit day' window.
        /// </summary>
        /// <param name="index">The index of the day to edit.</param>
        private void EditDay(int index)
        {
            // Ensure something is selected
            if (index == -1)
            {
                StatusBarText = "To edit a day, please select something first.";
                return;
            }

            OnEditDayRequested(index);
        }

        #endregion // Edit day

        #region Fix values

        // This command shows the 'fix inaccurate values' window.
        private ICommand _fixValuesCommand;
        public ICommand FixValuesCommand
        {
            get
            {
                if (_fixValuesCommand == null)
                {
                    _fixValuesCommand = new RelayCommand(param => this.FixValues());
                }
                return _fixValuesCommand;
            }
        }

        /// <summary>
        /// Create and show the 'fix inaccurate values' window.
        /// </summary>
        private void FixValues()
        {
            OnFixValuesRequested();
        }

        #endregion // Fix values

        #endregion // Commands

        #region Functions

        #region Locations

        /// <summary>
        /// Updates the list of locations to include a new one. Also notifies the change.
        /// </summary>
        /// <param name="newLocation">The new location to add.</param>
        private void UpdateLocations(string newLocation)
        {
            // reject invalid values
            if (newLocation == null || newLocation.Equals(string.Empty)) return;

            // Add the location if required
            if (!_locations.Contains(newLocation))
            {
                if (_locations.Count >= 5)
                {
                    _locations.RemoveAt(4);
                }

                _locations.Insert(0, newLocation);

                NotifyPropertyChanged("Locations");
            }
        }

        #endregion // Locations

        #region Days off and vacations

        /// <summary>
        /// Calculates the new values for Days Off and Vacations when a new day is added.
        /// </summary>
        /// <param name="type">The type of day added.</param>
        /// <param name="date">The date of the added day.</param>
        private void CalculateAddDay(DayType type, DateTime date)
        {
            // Get current values
            double newDaysOff = DaysOff;
            double newVacations = Vacations;

            // Determine new values
            switch(type)
            {
                case DayType.Offshore:
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) newDaysOff += 1d / 2d;
                    else newDaysOff += 1d / 3d;
                    break;
                case DayType.OffshoreBh:
                    newDaysOff += 1d + (1d / 3d);
                    break;
                case DayType.DayOff:
                    newDaysOff -= 1d;
                    break;
                case DayType.Vacation:
                    newVacations -= 1;
                    break;
            }

            // If this is the first day of a new month, calculate new vacations.
            if (date.Day == 1)
            {
                DateTime previousDay = date.AddDays(-1);
                newVacations += (28d / 365d) * DateTime.DaysInMonth(previousDay.Year, previousDay.Month);
            }

            // Set the values
            DaysOff = newDaysOff;
            Vacations = newVacations;
        }

        /// <summary>
        /// Calculates the new values for Days Off and Vacations when an existing day is removed.
        /// </summary>
        /// <param name="type">The type of day removed.</param>
        /// <param name="date">The date of the removed day.</param>
        private void CalculateRemoveDay(DayType type, DateTime date)
        {
            // Get current values
            double newDaysOff = DaysOff;
            double newVacations = Vacations;

            // Determine new values
            switch(type)
            {
                case DayType.Offshore:
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) newDaysOff -= 1d / 2d;
                    else newDaysOff -= 1d / 3d;
                    break;
                case DayType.OffshoreBh:
                    newDaysOff -= 1d + (1d / 3d);
                    break;
                case DayType.DayOff:
                    newDaysOff += 1d;
                    break;
                case DayType.Vacation:
                    newVacations += 1;
                    break;
            }

            // If this is the first day of a new month, calculate new vacations
            if (date.Day == 1)
            {
                DateTime previousDay = date.AddDays(-1);
                newVacations -= (28d / 365d) * DateTime.DaysInMonth(previousDay.Year, previousDay.Month);
            }

            // Set the values
            DaysOff = newDaysOff;
            Vacations = newVacations;
        }

        #endregion // Days off and vacations

        #region Day count

        /// <summary>
        /// Recalculates every day's count. This should be called only after the initial date changes.
        /// </summary>
        private void RecountDays()
        {
            _offshoreData.YearCounts.Clear();
            foreach (OffshoreDay day in _offshoreData.Days)
            {
                CountDay(day.DayType, day.Date);
            }
        }

        /// <summary>
        /// Counts a day in the counter.
        /// </summary>
        /// <param name="dayType">The type of day to count.</param>
        /// <param name="date">The date of the day to count.</param>
        private void CountDay(DayType dayType, DateTime date)
        {
            // If it is the first of january or there is nothing in the list, create a new OffshoreYearCount object.
            if (_offshoreData.YearCounts.Count == 0 || date.DayOfYear == 1)
                _offshoreData.YearCounts.Add(new OffshoreYearCount() { Year = date.Year });

            // Create or increment the required value
            if (_offshoreData.YearCounts.Last().Counter.ContainsKey(dayType))
                _offshoreData.YearCounts.Last().Counter[dayType] += 1;
            else
                _offshoreData.YearCounts.Last().Counter.Add(dayType, 1);
        }

        /// <summary>
        /// Removes a day from the counter.
        /// </summary>
        /// <param name="dayType">The type of day to uncount.</param>
        /// <param name="date">The date of the day to uncount.</param>
        private void UncountDay(DayType dayType, DateTime date)
        {
            // If it is the 1st of January, remove the latest OffshoreYearCount object from the list
            if (date.DayOfYear == 1) _offshoreData.YearCounts.RemoveAt(_offshoreData.YearCounts.Count - 1);

            // Otherwise, just decrease the value
            else _offshoreData.YearCounts.Last().Counter[dayType] -= 1;
        }

        #endregion // Day count

        #region Dialog handlers

        /// <summary>
        /// Handles the values returned by the 'options' dialog box.
        /// </summary>
        /// <param name="initialDate">The new initial date.</param>
        public void HandleOptionsDialog(DateTime initialDate)
        {
            // Set the initial date, and update the current view models
            _offshoreData.InitialDate = initialDate;
            DateTime runningDate = initialDate;
            foreach (MainWindowDayViewModel day in OffshoreDays)
            {
                day.Date = runningDate;
                runningDate = runningDate.AddDays(1);
            }

            // Recount the number of days
            RecountDays();

            // Update status bar
            NotifyPropertyChanged("Counter");
            _changesMade = true;
        }

        /// <summary>
        /// Handles the values returned by the 'edit day' dialog box.
        /// </summary>
        /// <param name="index">The index to change.</param>
        /// <param name="dayType">The new type of day to use.</param>
        /// <param name="description">The new description.</param>
        /// <param name="updateValues">Whether or not to update the days off and vacation values.</param>
        public void HandleEditDayDialog(int index, DayType dayType, string description, string location, bool updateValues)
        {
            // Get the day to edit
            MainWindowDayViewModel day = OffshoreDays[index];

            // Roll the now-invalid data back one day
            if (updateValues && (dayType != day.DayType))
            {
                CalculateRemoveDay(day.DayType, day.Date);
            }
            UncountDay(day.DayType, day.Date);

            // Update the values
            day.DayType = dayType;
            day.Description = description;
            day.Location = location;
            UpdateLocations(location);

            // Apply the new data calculations
            if (updateValues)
            {
                CalculateAddDay(day.DayType, day.Date);
            }
            CountDay(day.DayType, day.Date);

            // Notify changes
            NotifyPropertyChanged("Counter");

            // Update status bar
            StatusBarText = "Day edited.";
            _changesMade = true;
        }

        /// <summary>
        /// Handles the values returned by the 'edit inaccurate values' dialog box.
        /// </summary>
        /// <param name="daysOff">The new days off.</param>
        /// <param name="vacations">The new vacations.</param>
        public void HandleFixValuesDialog(double daysOff, double vacations)
        {
            DaysOff = daysOff;
            Vacations = vacations;

            // Update status bar
            StatusBarText = "Values have been edited.";
            _changesMade = true;
        }

        #endregion // Dialog handlers

        #endregion // Functions

        #region Events

        #region SaveCalled

        public event EventHandler<SaveEventArgs> SaveCalled;

        private void OnSaveCalled()
        {
            if (SaveCalled != null)
            {
                SaveCalled(this, new SaveEventArgs() { OffshoreData = _offshoreData });
            }
        }

        #endregion // SaveCalled

        #region NewDayAdded

        public event EventHandler<EventArgs> NewDayAdded;

        private void OnNewDayAdded()
        {
            if (NewDayAdded != null)
            {
                NewDayAdded(this, EventArgs.Empty);
            }
        }

        #endregion // NewDayAdded

        #region AboutRequested

        public event EventHandler<EventArgs> AboutRequested;

        private void OnAboutRequested()
        {
            if (AboutRequested != null)
            {
                AboutRequested(this, EventArgs.Empty);
            }
        }

        #endregion // AboutRequested

        #region OptionsRequested

        public event EventHandler<OptionsEventArgs> OptionsRequested;

        private void OnOptionsRequested()
        {
            if (OptionsRequested != null)
            {
                OptionsRequested(this, new OptionsEventArgs { InitialDate = _offshoreData.InitialDate });
            }
        }

        #endregion // OptionsRequested

        #region ViewDayRequested

        public event EventHandler<ViewDayEventArgs> ViewDayRequested;

        private void OnViewDayRequested(int index)
        {
            if (ViewDayRequested != null)
            {
                OffshoreDay day = _offshoreData.Days[index];
                ViewDayRequested(this, new ViewDayEventArgs
                {
                    Day = _offshoreData.Days[index]
                });
            }
        }

        #endregion // ViewDayRequested

        #region EditDayRequested

        public event EventHandler<EditDayEventArgs> EditDayRequested;

        private void OnEditDayRequested(int index)
        {
            if (EditDayRequested != null)
            {
                OffshoreDay day = _offshoreData.Days[index];
                EditDayRequested(this, new EditDayEventArgs
                {
                    Index = index,
                    Day = _offshoreData.Days[index],
                    Locations = _locations.ToList()
                });
            }
        }

        #endregion // EditDayRequested

        #region FixValuesRequested

        public event EventHandler<FixValuesEventArgs> FixValuesRequested;

        private void OnFixValuesRequested()
        {
            if (FixValuesRequested != null)
            {
                FixValuesRequested(this, new FixValuesEventArgs() { DaysOff = _offshoreData.DaysOff, Vacations = _offshoreData.Vacations });
            }
        }

        #endregion // FixValuesRequested

        #endregion // Events

    }
}
