using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SheetDice.Models;
using SheetDice.Services;
using SheetDice.ViewModels.Base;
using SheetDice.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace SheetDice.ViewModels.Inventario
{
    public partial class InventoryViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Item> Equipment { get; set; }

        public InventoryViewModel()
        {
            Equipment = new ObservableRangeCollection<Item>();
            _ = LoadInventory();
        }

        [ObservableProperty]
        private Item itemSelected;

        [ObservableProperty]
        private string weight = "0";

        [ICommand]
        private async Task ModifyQuantity(Item item)
        {
            string quantity = await Application.Current.MainPage.DisplayPromptAsync("Modifica Quantità", "Quanti ne hai?", initialValue: item.Quantity.ToString());
            if (quantity == null || quantity.Equals(item.Quantity.ToString()))
            {
                return;
            }
            item.Quantity = int.Parse(quantity);
            await ItemDatabase.UpdateItem(item);
            await Refresh();
        }

        [ICommand]
        private async Task AddItem()
        {
            var route = $"{nameof(ItemCreationPage)}";
            await Shell.Current.GoToAsync(route);
        }

        [ICommand]
        private async Task RemoveItem(Item item)
        {
            await ItemDatabase.RemoveItem(item.Id);
            await Refresh();
        }

        [ICommand]
        private async Task SelectedItem(object obj)
        {
            if (obj is not Item item)
            {
                return;
            }
            ItemSelected = null;
            bool risposta = await Application.Current.MainPage.DisplayAlert(item.Name, TextDescription(item), "Modifica", "Ok");
            if (risposta)
            {
                var route = $"{nameof(ItemModifyPage)}?ItemId={item.Id}";
                await Shell.Current.GoToAsync(route);
            }
        }

        [ICommand]
        private async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Equipment.Clear();
            await LoadInventory();
            IsBusy = false;
        }

        private async Task LoadInventory()
        {
            var items = await ItemDatabase.GetItems();
            Equipment.AddRange(items);
            Weight = EvaluateWeight();
        }

        private string EvaluateWeight()
        {
            double weight = 0;
            foreach (Item item in Equipment)
                weight += item.Weight * item.Quantity;
            return weight.ToString();
        }

        private string TextDescription(Item item)
        {
            StringBuilder sb = new();
            sb.Append(item.Category.ToString()).Append("\n");
            sb.Append("Weight: ").Append(item.Weight.ToString()).Append("\n");
            sb.Append("Quantity: ").Append(item.Quantity.ToString()).Append("\n");
            return sb.ToString();
        }

    }
}
