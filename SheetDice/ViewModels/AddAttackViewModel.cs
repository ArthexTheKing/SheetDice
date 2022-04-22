using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SheetDice.Models;
using SheetDice.Services;
using SheetDice.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels
{
    public partial class AddAttackViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string damage = string.Empty;

        [ObservableProperty]
        private StatType statSelected = StatType.None;

        [ObservableProperty]
        private DamageType typeSelected = DamageType.None;

        [ObservableProperty]
        private bool isMagic = false;

        public List<StatType> StatTypes { get; set; }

        public List<DamageType> DamageTypes { get; set;}

        public AddAttackViewModel()
        {
            StatTypes = new List<StatType>();
            DamageTypes = new List<DamageType>();
            LoadEnumListType();
        }

        [ICommand]
        private async Task AddAttack()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Name", "Type a name", "Close");
                return;
            }
            if (TypeSelected == DamageType.None)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Type", "Type a name for the damage", "Close");
                return;
            }

            if(StatSelected == StatType.None)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Statistic", "Type a name for the stat", "Close");
                return;
            }

            Attack attack = CreateAttack();
            await AttacksDatabase.AddAttack(attack);
            await Shell.Current.GoToAsync("..");
        }

        private void LoadEnumListType()
        {
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
                StatTypes.Add(statType);

            foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
                    DamageTypes.Add(damageType);
        }
        private Attack CreateAttack()
        {
            Attack attack = new()
            {
                Name = Name,
                Damage = Damage,
                Stat = StatSelected,
                Type = TypeSelected,
                IsMagic = IsMagic
            };
            return attack;
        }

        [ICommand]
        private async Task GoBack()
        {
            bool risposta = await Application.Current.MainPage.DisplayAlert("Discard", "Are you sure you want to discard this item", "Keep editing", "Discard changes");
            if (!risposta)
                await Shell.Current.GoToAsync("..");
        }
    }
}
