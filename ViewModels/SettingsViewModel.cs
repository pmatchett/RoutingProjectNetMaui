using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutingProjectNet.src;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RoutingProjectNet.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        public SettingsViewModel()
        {
            XDim = 50;
            YDim = 50;
            ObsPercent = 0.3;
            Confirmation = "";
        }

        [ObservableProperty]
        int _xDim;

        [ObservableProperty]
        int _yDim;

        [ObservableProperty]
        double _obsPercent;

        [ObservableProperty]
        string _confirmation;

        [RelayCommand]
        public void SaveSettings()
        {
            RoutingMap.SetXDimSettings(XDim);
            RoutingMap.SetYDimSettings(YDim);
            RoutingMap.SetObsPercentSettings(ObsPercent);
            Confirmation = "Settings Saved";
        }
    }
}
