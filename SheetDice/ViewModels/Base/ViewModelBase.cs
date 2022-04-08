
using CommunityToolkit.Mvvm.ComponentModel;

namespace SheetDice.ViewModels.Base
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private string title = string.Empty;

    }
}
