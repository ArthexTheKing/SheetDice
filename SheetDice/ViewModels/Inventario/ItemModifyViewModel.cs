using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using SheetDice.Models;
using SheetDice.Services.Repository;
using SheetDice.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels.Inventario
{
    [QueryProperty(nameof(LoadItem), "SendItem")]
    public partial class ItemModifyViewModel : ViewModelBase
    {

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string weight;

        [ObservableProperty]
        private string value;

        [ObservableProperty]
        private string quantity;

        [ObservableProperty]
        ItemType categorySelected;

        [ObservableProperty]
        private bool isMagical;

        [ObservableProperty]
        private string description;

        public string LoadItem
        {
            set 
            { 
                Item item = JsonConvert.DeserializeObject<Item>(value);
                Name = item.Name;
                Weight = item.Weight.ToString();
                Value = item.Value.ToString();
                Quantity = item.Quantity.ToString();
                CategorySelected = item.Category;
                IsMagical = item.IsMagical;
                Description = item.Description;
                toModify = item;
            }
        }

        private Item toModify;
        private readonly LocalDB<Item> itemDatabase;

        public List<ItemType> ItemTypes { get; set; }
        
        public ItemModifyViewModel()
        {
            ItemTypes = new List<ItemType>();
            itemDatabase = new LocalDB<Item>();
            LoadEnumList();
        }

        [ICommand]
        async Task GoBack()
        {
            bool risposta = await Application.Current.MainPage.DisplayAlert("Discard", "Are you sure you want to discard this item", "Keep editing", "Discard changes");
            if (!risposta)
                await Shell.Current.GoToAsync("..");
        }

        [ICommand]
        async Task Save()
        {
            toModify.Name = Name;
            toModify.Weight = double.Parse(Weight);
            toModify.Value = int.Parse(Value);
            toModify.Quantity = int.Parse(Quantity);
            toModify.Category = CategorySelected;
            toModify.IsMagical = IsMagical;
            toModify.Description = Description;
            await itemDatabase.Update(toModify);
            await Shell.Current.GoToAsync("..");
        }

        private void LoadEnumList()
        {
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
                ItemTypes.Add(itemType);
        }

    }
}
