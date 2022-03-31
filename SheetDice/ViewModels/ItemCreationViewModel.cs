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
        private const string Titolo = "Discard";
        private const string Descrizione = "Are you sure you want to discard this item";
        private const string Accetto = "Keep editing";
        private const string Rifiuto = "Discard changes";

        string name = "";
        string weight = "0.0";
        string value = "0";
        string quantity = "1";
        ItemType categorySelected;
        bool isMagical = false;
        string description = "";

        public string NameEntry { get => name; set => SetProperty(ref name, value); }
        public string WeightEntry { get => weight; set => SetProperty(ref weight, value); }
        public string ValueEntry { get => value; set => SetProperty(ref value, value); }
        public string QuantityEntry { get => quantity; set => SetProperty(ref quantity, value); }
        public ItemType CategorySelected { get => categorySelected; set => SetProperty(ref categorySelected, value); }
        public bool IsMagicalCheck { get => isMagical; set => SetProperty(ref isMagical, value); }
        public string DescriptionEditor { get => description; set => SetProperty(ref description, value); }
        public List<ItemType> ItemTypes { get; set; }
        public AsyncCommand CreateCommand { get; }
        public AsyncCommand GoBackCommand { get; }

        public ItemCreationViewModel()
        {
            ItemTypes = new List<ItemType>();
            LoadEnumList();

            CreateCommand = new AsyncCommand(Create);
            GoBackCommand = new AsyncCommand(GoBack);
        }

        async Task GoBack()
        {
            bool risposta = await Application.Current.MainPage.DisplayAlert(Titolo, Descrizione, Accetto, Rifiuto);
            if (!risposta)
                await Shell.Current.GoToAsync("..");
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
                Description = DescriptionEditor,
                IsEquipped = true
            };

            await ItemDatabase.AddItem(item);

            await Shell.Current.GoToAsync("..");
        }

        private void LoadEnumList()
        {
            foreach(ItemType itemType in Enum.GetValues(typeof(ItemType)))
                ItemTypes.Add(itemType);
        }
    }
}
