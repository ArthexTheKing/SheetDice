using MvvmHelpers;
using SheetDice.Models;

namespace SheetDice.ViewModels
{
    public class InventoryViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Equipment { get; set; }

        public InventoryViewModel()
        {
            Equipment = new ObservableRangeCollection<Item>
            {
                new Item("spada", "descrizione", "arma"),
                new Item("scudo", "descrizione", "arma"),
                new Item("armatura", "descrizione", "armatura")
            };

        }
    }
}
