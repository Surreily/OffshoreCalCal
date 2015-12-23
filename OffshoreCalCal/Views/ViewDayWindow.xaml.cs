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
using System.Windows.Shapes;

namespace OffshoreCalCal.Views
{
    /// <summary>
    /// Interaction logic for ViewDayWindow.xaml
    /// </summary>
    public partial class ViewDayWindow : Window
    {
        public ViewDayWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close button click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
