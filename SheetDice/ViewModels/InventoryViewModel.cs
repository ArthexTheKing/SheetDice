using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using SheetDice.Services;
using SheetDice.Views;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels
{
    public class InventoryViewModel : BaseViewModel
    {
        Item itemSelected;
        string weight = "0";

        public Item ItemSelected 
        { 
            get => itemSelected; 
            set => SetProperty(ref itemSelected, value); 
        }
        public string Weight 
        { 
            get => weight; 
            set => SetProperty(ref weight, value); 
        }
        
        public ObservableRangeCollection<Item> Equipment { get; set; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand AddItemCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Item> RemoveCommand { get; }
        public AsyncCommand<Item> ModifyQuantityCommand { get; }

        public InventoryViewModel()
        {
            Equipment = new ObservableRangeCollection<Item>();

            Task task = LoadInventory();

            SelectedCommand = new AsyncCommand<object>(Selected);
            AddItemCommand = new AsyncCommand(AddItem);
            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<Item>(RemoveItem);
            ModifyQuantityCommand = new AsyncCommand<Item>(ModifyQuantity);
        }

        async Task ModifyQuantity(Item item)
        {
            string quantity;
            do
            {
                quantity = await Application.Current.MainPage.DisplayPromptAsync("Modifica Quantità", "Quanti ne hai?", initialValue: item.Quantity.ToString());
                if (string.IsNullOrEmpty(quantity))
                    await Application.Current.MainPage.DisplayAlert("Errore", "Non puoi impostare una quantità nulla", "ok");
            }
            while(string.IsNullOrEmpty(quantity));
           
            item.Quantity = int.Parse(quantity);
            await ItemDatabase.UpdateItem(item);
            await Refresh();
        }

        async Task AddItem()
        {
            var route = $"{nameof(ItemCreationPage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task RemoveItem(Item item)
        {
            await ItemDatabase.RemoveItem(item.Id);
            await Refresh();
        }

        async Task Refresh()
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

        async Task Selected(object obj)
        {
            if (!(obj is Item item))
                return;
            ItemSelected = null;
            await Application.Current.MainPage.DisplayAlert(item.Name, TextDescription(item), "Ok");
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
            StringBuilder sb = new StringBuilder();
            sb.Append(item.Category.ToString()).Append("\n");
            sb.Append("Weight: ").Append(item.Weight.ToString()).Append("\n");
            sb.Append("Quantity: ").Append(item.Quantity.ToString()).Append("\n");
            return sb.ToString();
        }
        
    }
}
