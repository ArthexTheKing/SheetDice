using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using SheetDice.Services;
using SheetDice.Views.Inventory;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels.Inventory
{
    public class InventoryViewModel : BaseViewModel
    {
        Item itemSelected;
        public Item ItemSelected
        {
            get => itemSelected;
            set => SetProperty(ref itemSelected, value);
        }
        public ObservableRangeCollection<Item> Equipment { get; set; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand AddItemCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Item> RemoveCommand { get; }

        public InventoryViewModel()
        {
            Equipment = new ObservableRangeCollection<Item>();

            SelectedCommand = new AsyncCommand<object>(Selected);
            AddItemCommand = new AsyncCommand(AddItem);
            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<Item>(RemoveItem);
        }

        async Task AddItem()
        {
            var route = $"{nameof(ItemCreationPage)}";
            await Shell.Current.GoToAsync(route);
            /*
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Inserisci il nome dell'oggetto");
            var descrizione = await App.Current.MainPage.DisplayPromptAsync("Description", "Inserisci la descrizione");
            Item item = new Item() { Name = name, Description = descrizione, Value = 0 };
            await ItemDatabase.AddItem(item);
            await Refresh();
            */
        }

        async Task RemoveItem(Item item)
        {
            await ItemDatabase.RemoveItem(item.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(500);
            Equipment.Clear();
            var items = await ItemDatabase.GetItems();
            Equipment.AddRange(items); 
            IsBusy = false;
        }

        

        async Task Selected(object obj)
        {
            if (!(obj is Item item))
                return;
            ItemSelected = null;
            string description = this.TextDescription(item);
            await Application.Current.MainPage.DisplayAlert(item.Name, description, "close");
        }

        private string TextDescription (Item item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.Category.ToString()).Append("\n");
            sb.Append("Weight: ").Append(item.Weight.ToString()).Append("\n");
            sb.Append("Quantity: ").Append(item.Quantity.ToString()).Append("\n");
            return sb.ToString();
        }

    }
}
