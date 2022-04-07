using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

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
