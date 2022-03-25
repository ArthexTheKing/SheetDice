using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
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

        public InventoryViewModel()
        {
            Equipment = new ObservableRangeCollection<Item>
            {
                new Item() { Name = "Spada", Description = "descrizione", Value = 10 },
                new Item() { Name = "Piccone", Description = "descrizione", Value = 10 },
                new Item() { Name = "Ascia", Description = "descrizione", Value = 10 },
                new Item() { Name = "Pala", Description = "descrizione", Value = 10 }
            };

            SelectedCommand = new AsyncCommand<object>(Selected);
            AddItemCommand = new AsyncCommand(AddItem);
        }

        async Task AddItem()
        {
            var route = $"{nameof(ItemCreationPage)}";
            await Shell.Current.GoToAsync(route);
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
