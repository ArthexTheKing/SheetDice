using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;

namespace SheetDice.ViewModels
{
   public class AttackViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Attack> Attacks { get; set;}

        public AsyncCommand RefreshComand { get; }

        public AttackViewModel()
        {
            Attacks = new ObservableRangeCollection<Attack>
            {
                new Attack() { Name = "Broadsword", Damage = "2d6", Type = "Slashing"},
                new Attack() { Name = "Spear", Damage = "1d6", Type = "Piercing"},
                new Attack() {Name = "Dagger", Damage = "1d4", Type = "Piercing"}
            };
        }

    }
} 
