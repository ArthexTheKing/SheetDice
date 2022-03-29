using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels.Inventory
{
    public class ItemCreationViewModel : BaseViewModel
    {
        public AsyncCommand AddItemCommand { get; }

        public ItemCreationViewModel()
        {
            AddItemCommand = new AsyncCommand(AddItem);
        }

        async Task AddItem()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
