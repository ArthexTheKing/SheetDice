using MvvmHelpers;
using SheetDice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SheetDice.ViewModels
{
    public class ItemCreationViewModel : BaseViewModel
    {
        public IList<ItemType> ItemTypes { get; set; }
        public ItemCreationViewModel()
        {

        }
    }
}
