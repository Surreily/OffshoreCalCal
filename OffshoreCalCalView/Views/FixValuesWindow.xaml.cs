using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OffshoreCalCalView.Views
{
    /// <summary>
    /// The fix values window allows the user to fix the running totals
    /// (vacations and days off) if they go wrong for any reason.
    /// </summary>
    public partial class FixValuesWindow : Window
    {
        public FixValuesWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Accept button click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }  
    }
}
