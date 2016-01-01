using OffshoreCalCalModel.Models;

namespace OffshoreCalCalViewModel.CustomEventArgs
{
    /// <summary>
    /// A custom EventArgs class that contains an OffshoreData object, which is
    /// used when saving the data.
    /// </summary>
    public class SaveEventArgs
    {
        public OffshoreData OffshoreData { get; set; }
    }
}
