using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SheetDice.ViewModels
{
    public class CharacterSelectionViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Character> Characters { get; }
        public AsyncCommand RefreshCommand { get; }

        public CharacterSelectionViewModel()
        {
            Characters = new ObservableRangeCollection<Character>
            {
                new Character("Lilia"),
                new Character("Albert"),
                new Character("Baldwin")
            };

            RefreshCommand = new AsyncCommand(Refresh);

        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}
