using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using SheetDice.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels
{
    public class ItemCreationViewModel : BaseViewModel
    {
        string name;
        string weight = "0.0";
        string value = "0";
        string quantity = "1";
        ItemType categorySelected;
        bool isMagical = false;

        public string NameEntry { get => name; set => SetProperty(ref name, value); }
        public string WeightEntry { get => weight; set => SetProperty(ref weight, value); }
        public string ValueEntry { get => value; set => SetProperty(ref value, value); }
        public string QuantityEntry { get => quantity; set => SetProperty(ref quantity, value); }
        public ItemType CategorySelected { get => categorySelected; set => SetProperty(ref categorySelected, value); }
        public bool IsMagicalCheck { get => isMagical; set => SetProperty(ref isMagical, value); }
        public List<ItemType> ItemTypes { get; set; }
        public AsyncCommand CreateCommand { get; }
        public ItemCreationViewModel()
        {
            ItemTypes = new List<ItemType>();
            LoadEnumList();

            CreateCommand = new AsyncCommand(Create);
        }

        async Task Create()
        {
            if (string.IsNullOrWhiteSpace(NameEntry) || CategorySelected == ItemType.None)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Item", "type a name for the item", "close");
                return;
            }

            Item item = new Item()
            {
                Name = NameEntry,
                Weight = double.Parse(WeightEntry),
                Value = int.Parse(ValueEntry),
                Quantity = int.Parse(QuantityEntry),
                Category = CategorySelected,
                IsMagical = IsMagicalCheck,
                Description = ""
            };

            await ItemDatabase.AddItem(item);
        }

        private void LoadEnumList()
        {
            foreach(ItemType itemType in Enum.GetValues(typeof(ItemType)))
                ItemTypes.Add(itemType);
        }
    }
}
