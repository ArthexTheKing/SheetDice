using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SheetDice.Models;
using SheetDice.Services;
using SheetDice.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels.Inventario
{
    public partial class ItemCreationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string name = string.Empty;
        
        [ObservableProperty]
        private string weight = "0.0";
        
        [ObservableProperty]
        private string value = "0";
        
        [ObservableProperty]
        private string quantity = "1";
        
        [ObservableProperty]
        private ItemType categorySelected = ItemType.None;
        
        [ObservableProperty]
        private bool isMagical = false;
        
        [ObservableProperty]
        private string description = string.Empty;

        public List<ItemType> ItemTypes { get; set; }
        
        public ItemCreationViewModel()
        {
            ItemTypes = new List<ItemType>();
            LoadEnumList();
        }

        [ICommand]
        private async Task GoBack()
        {
            bool risposta = await Application.Current.MainPage.DisplayAlert("Discard", "Are you sure you want to discard this item", "Keep editing", "Discard changes");
            if (!risposta)
                await Shell.Current.GoToAsync("..");
        }
        
        [ICommand]
        private async Task Create()
        {
            if (string.IsNullOrWhiteSpace(Name) || CategorySelected == ItemType.None)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Item", "type a name for the item", "close");
                return;
            }
            Item item = CreateItem();
            await ItemDatabase.AddItem(item);
            await Shell.Current.GoToAsync("..");
        }

        private void LoadEnumList()
        {
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
                ItemTypes.Add(itemType);
        }

        private Item CreateItem()
        {
            Item item = new()
            {
                Name = Name,
                Weight = double.Parse(Weight),
                Value = int.Parse(Value),
                Quantity = int.Parse(Quantity),
                Category = CategorySelected,
                IsMagical = IsMagical,
                Description = Description,
                IsEquipped = true
            };
            return item;
        }
    }
}
