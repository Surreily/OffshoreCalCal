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

namespace OffshoreCalCalView.Views
{
    /// <summary>
    /// The edit day window allows the user to edit a given day's properties.
    /// </summary>
    public partial class EditDayWindow : Window
    {
        public EditDayWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the accept button click event.
        /// </summary>
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
