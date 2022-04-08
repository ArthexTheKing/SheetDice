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
using Command = MvvmHelpers.Commands.Command;

namespace SheetDice.ViewModels
{
   public class AttackViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Attack> Attacks { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand AddCommand { get; }

        public AsyncCommand<Attack> RemoveCommand { get; }

        public AsyncCommand<Attack> FavoriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public AttackViewModel()
        {
            Attacks = new ObservableRangeCollection<Attack>();

            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            FavoriteCommand = new AsyncCommand<Attack>(Favorite);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Attack>(Remove);
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            var attacks = await AttacksDatabase.GetAttack();
            Attacks.Clear();
            Attacks.AddRange(attacks);
            IsBusy = false;
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Attack name");
            var type = await App.Current.MainPage.DisplayPromptAsync("Type", "Attack type");
            var damage = await App.Current.MainPage.DisplayPromptAsync("Damage", "Damage of the attack");
            await AttacksDatabase.AddAttack(name, type, damage);
            await Refresh();
        }

        async Task Remove(Attack attack)
        {
            await AttacksDatabase.RemoveAttack(attack.Id);
            await Refresh();
        }

        Attack selectedAttack;
        public Attack SelectedAttack
        {
            get => selectedAttack;
            set => SetProperty(ref selectedAttack, value);
        }

        async Task Selected(object args)
        {
            var attack = args as Attack;
            if (attack == null)
                return;

            SelectedAttack = null;
            await Application.Current.MainPage.DisplayAlert("Selected", attack.Name, "Ok");
        }

        async Task Favorite(Attack attack)
        {
            if (attack == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Selected", attack.Name, "Ok");
        }
    }
} 
