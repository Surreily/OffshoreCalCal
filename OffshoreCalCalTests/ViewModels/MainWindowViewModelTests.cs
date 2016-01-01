using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCalTests.ViewModels
{
    [TestClass]
    public class MainWindowViewModelTests
    {

        /// <summary>
        /// Tests if a failure to load existing data will result in the
        /// creation of a default object, and that the new object has the
        /// correct default values.
        /// </summary>
        [TestMethod]
        public void MainWindowViewModel_NullPassed()
        {
            // Arrange
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(null);

            // Assert% 
            Assert.AreEqual(mainWindowViewModel.DaysOff, 0, "mainWindowViewModel.DaysOff is not 0.");
            Assert.AreEqual(mainWindowViewModel.Vacations, 0, "mainWindowViewModel.Vacations is not 0.");
        }
    }
}
