using OffshoreCalCal.Models;
using OffshoreCalCal.ViewModels;
using OffshoreCalCal.ViewModels.CustomEventArgs;
using OffshoreCalCal.ViewModels.DataStorage;
using OffshoreCalCalView.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OffshoreCalCalView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // The file location
        private const string fileLocation = "data.xml";

        // The main window's view model
        private MainWindowViewModel mwvm;
        private MainWindow mainWindow;

        /// <summary>
        /// The startup method. This creates the main window and loads the data.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The application arguments.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load the data
            DataLoader loader = new DataLoader(fileLocation);
            OffshoreData data;
            try
            {
                data = loader.Load();
            }
            catch (FileNotFoundException)
            {
                // The specified file was not found, let the MainWindowViewModel handle a null value
                data = null;
            }
            catch
            {
                // Something else went wrong, throw the exception
                throw;
            }

            // Create the window and view model
            mainWindow = new MainWindow();
            mwvm = new MainWindowViewModel(data);
            mainWindow.DataContext = mwvm;

            // Subscribe to the events in the view model
            mwvm.AboutRequested += OnAboutRequested;
            mwvm.OptionsRequested += OnOptionsRequested;
            mwvm.ViewDayRequested += OnViewDayRequested;
            mwvm.EditDayRequested += OnEditDayRequested;
            mwvm.FixValuesRequested += OnFixValuesRequested;
            mwvm.SaveCalled += OnSaveCalled;

            // Show window and scroll to bottom
            mainWindow.Show();
            mainWindow.ScrollToBottom();

            // Have the window subscribe to the view model's day added event
            mwvm.NewDayAdded += mainWindow.OnNewDayAdded;
        }

        /// <summary>
        /// Handle the opening of the About dialog.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs object (always empty and unused).</param>
        private void OnAboutRequested(object sender, EventArgs e)
        {
            // Create and display the window and view model
            AboutWindow window = new AboutWindow();
            window.Owner = mainWindow;
            AboutWindowViewModel vm = new AboutWindowViewModel();
            window.DataContext = vm;
            window.ShowDialog();
        }

        /// <summary>
        /// Handle the opening of the Options dialog.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs object.</param>
        private void OnOptionsRequested(object sender, OptionsEventArgs e)
        {
            // Create and display the window and view model
            OptionsWindow window = new OptionsWindow();
            window.Owner = mainWindow;
            OptionsWindowViewModel vm = new OptionsWindowViewModel(e.InitialDate);
            window.DataContext = vm;

            if (window.ShowDialog() == true)
            {
                mwvm.HandleOptionsDialog(vm.InitialDate);
            }
        }

        /// <summary>
        /// Handles the opening of the View Day dialog.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs object.</param>
        private void OnViewDayRequested(object sender, ViewDayEventArgs e)
        {
            // Create and display the window and view model
            ViewDayWindow window = new ViewDayWindow();
            window.Owner = mainWindow;
            ViewDayWindowViewModel vm = new ViewDayWindowViewModel(e.Day);
            window.DataContext = vm;
            window.ShowDialog();
        }

        /// <summary>
        /// Handle the opening of the Edit Day dialog.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs object.</param>
        private void OnEditDayRequested(object sender, EditDayEventArgs e)
        {
            // Create and display the window and view model
            EditDayWindow window = new EditDayWindow();
            window.Owner = mainWindow;
            EditDayWindowViewModel vm = new EditDayWindowViewModel(e.Index, e.Day, e.Locations);
            window.DataContext = vm;
            if (window.ShowDialog() == true)
            {
                mwvm.HandleEditDayDialog(vm.Index, vm.DayType, vm.Description, vm.TxtLocation, vm.UpdateValues);
            }
        }

        /// <summary>
        /// Handle the opening of the Fix Values dialog.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs object.</param>
        private void OnFixValuesRequested(object sender, FixValuesEventArgs e)
        {
            // Create and display the window and view model
            FixValuesWindow window = new FixValuesWindow();
            window.Owner = mainWindow;
            FixValuesWindowViewModel vm = new FixValuesWindowViewModel(e.DaysOff, e.Vacations);
            window.DataContext = vm;
            if (window.ShowDialog() == true)
            {
                mwvm.HandleFixValuesDialog(Convert.ToDouble(vm.DaysOff), Convert.ToDouble(vm.Vacations));
            }
        }

        /// <summary>
        /// Saves the data passed in the SaveEventArgs object to the file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs with the data to save.</param>
        private void OnSaveCalled(object sender, SaveEventArgs e)
        {
            // Save the data
            try
            {
                DataSaver saver = new DataSaver(fileLocation, e.OffshoreData);
                saver.Save();
            }
            catch
            {
                throw;
            }
        }
    }
}