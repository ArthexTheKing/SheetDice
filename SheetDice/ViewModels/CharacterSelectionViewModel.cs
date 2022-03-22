using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
