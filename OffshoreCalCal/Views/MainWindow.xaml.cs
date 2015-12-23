using OffshoreCalCal.ViewModels;
using OffshoreCalCal.ViewModels.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OffshoreCalCal.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A method that is called when the NewDayAdded event is raised.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs (not used).</param>
        public void OnNewDayAdded(object sender, EventArgs e)
        {
            ScrollToBottom();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)DataContext;
            if (vm.ChangesMade)
            {
                MessageBoxResult result = MessageBox.Show("Exit without saving?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
            }
                
        }

        /// <summary>
        /// Scrolls to the bottom of the DataGrid.
        /// </summary>
        public void ScrollToBottom()
        {
            if (dataGrid.Items.Count > 0)
            {
                var border = VisualTreeHelper.GetChild(dataGrid, 0) as Decorator;
                if (border != null)
                {
                    var scroll = border.Child as ScrollViewer;
                    if (scroll != null)
                    {
                        scroll.ScrollToEnd();
                    }
                }
            }
        }
    }
}
