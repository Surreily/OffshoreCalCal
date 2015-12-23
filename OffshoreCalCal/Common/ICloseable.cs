using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OffshoreCalCal.Common
{
    /// <summary>
    /// Interface that represents a closeable window.
    /// </summary>
    interface ICloseable
    {
        // The closeable event
        event EventHandler<EventArgs> RequestClose;
    }
}
