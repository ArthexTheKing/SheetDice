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
using Xamarin.Forms;
using SheetDice.Services;

namespace SheetDice.ViewModels
{
   public class AttackViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Attack> Attacks { get; set;}

        public AsyncCommand RefreshComand { get; }

        public AsyncCommand AddComand { get; }

        public AsyncCommand RemoveComand { get; }

        public AsyncCommand<Attack> FavoriteComand { get; }
        public AsyncCommand<Attack> SelectedComand { get; }

        public AttackViewModel()
        {
            Attacks = new ObservableRangeCollection<Attack>
            {
                new Attack() { Name = "Broadsword", Damage = "2d6", Type = "Slashing"},
                new Attack() { Name = "Spear", Damage = "1d6", Type = "Piercing"},
                new Attack() {Name = "Dagger", Damage = "1d4", Type = "Piercing"}
            };

            RefreshComand = new AsyncCommand(Refresh);
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            var attacks = await AttacksDatabase.GetAttack();
            Attacks.AddRange(attacks);
            IsBusy = false;
        }

        async Task Remove(Attack attack)
        {
            await AttacksDatabase.RemoveAttack(attack.Id);
            await Refresh();
        }

        Attack previouslySelected;
        Attack selectedAttack;
        public Attack SelectedAttack
        {
            get => selectedAttack;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "Ok");
                    previouslySelected = value;
                    value = null;
                }
                selectedAttack = value;
                OnPropertyChanged();
            }
        }

        async Task Favorite(Attack attack)
        {
            if (attack == null)
                return;
            Application.Current.MainPage.DisplayAlert("Favorite", attack.Name, "Ok");
        }

    }
} 
