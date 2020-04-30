using TidBit.ViewModels.Base;
using Xamarin.Essentials;

namespace TidBit.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public string tidbitVersion { get; set; }

        public SettingsViewModel()
        {
            tidbitVersion = AppInfo.VersionString;
        }
    }
}
