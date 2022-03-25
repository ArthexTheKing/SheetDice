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
                new Item() { Name = "spada", Description = "descrizione", Value = 10 },
                new Item() { Name = "piccone", Description = "descrizione", Value = 10 },
                new Item() { Name = "ascia", Description = "descrizione", Value = 10 },
                new Item() { Name = "pala", Description = "descrizione", Value = 10 }
            };

        }
    }
}
