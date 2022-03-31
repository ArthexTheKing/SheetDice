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
        
        string cp = "0";
        string sp = "0";
        string ep = "0";
        string gp = "0";
        string pp = "0";
        string weight = "0";

        public string Cp
        {
            get => cp; 
            set 
            {
                SetProperty(ref cp, value);
                Weight = EvaluateWeight();  
            }
        }
        public string Sp
        {
            get => sp;
            set
            {
                SetProperty (ref sp, value);
                Weight = EvaluateWeight();
            }
        }
        public string Ep
        {
            get => ep;
            set
            {
                SetProperty(ref ep, value);
                Weight = EvaluateWeight();
            }
        }
        public string Gp
        {
            get => gp;
            set
            {
                SetProperty(ref gp, value);
                Weight = EvaluateWeight();
            }
        }
        public string Pp
        {
            get => pp;
            set
            {
                SetProperty(ref pp, value);
                Weight = EvaluateWeight();
            }
        }
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
            Weight = EvaluateWeight();
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

        private string EvaluateWeight()
        {
            double weight = 0;
            foreach (Item item in Equipment)
                weight += item.Weight;
            weight += double.Parse(Cp) * 0.02;
            weight += double.Parse(Sp) * 0.02;
            weight += double.Parse(Ep) * 0.02;
            weight += double.Parse(Gp) * 0.02;
            weight += double.Parse(Pp) * 0.02;
            return weight.ToString();
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
