using MvvmHelpers.Commands;
using SheetDice.Models;
using SheetDice.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels.Inventario
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class ItemModifyViewModel : ViewModelBase
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

        string itemId;
        public string ItemId
        {
            get => itemId;
            set => SetProperty(ref itemId, value);
        }

        public string NameEntry { get => name; set => SetProperty(ref name, value); }
        public string WeightEntry { get => weight; set => SetProperty(ref weight, value); }
        public string ValueEntry { get => value; set => SetProperty(ref value, value); }
        public string QuantityEntry { get => quantity; set => SetProperty(ref quantity, value); }
        public ItemType CategorySelected { get => categorySelected; set => SetProperty(ref categorySelected, value); }
        public bool IsMagicalCheck { get => isMagical; set => SetProperty(ref isMagical, value); }
        public string DescriptionEditor { get => description; set => SetProperty(ref description, value); }

        public List<ItemType> ItemTypes { get; set; }
        public AsyncCommand SaveCommand { get; }
        public AsyncCommand GoBackCommand { get; }

        public ItemModifyViewModel()
        {
            ItemTypes = new List<ItemType>();
            LoadEnumList();

            SaveCommand = new AsyncCommand(Save);
            GoBackCommand = new AsyncCommand(GoBack);
        }

        async Task GoBack()
        {
            bool risposta = await Application.Current.MainPage.DisplayAlert("Discard", "Are you sure you want to discard this item", "Keep editing", "Discard changes");
            if (!risposta)
                await Shell.Current.GoToAsync("..");
        }

        async Task Save()
        {
            await Shell.Current.GoToAsync("..");
        }

        private void LoadEnumList()
        {
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
                ItemTypes.Add(itemType);
        }

    }
}
