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
        private DamageType typeSelected = DamageType.None;

        [ObservableProperty]
        private bool isMagic = false;

        public List<DamageType> DamageTypes { get; set;}

        public AddAttackViewModel()
        {
            DamageTypes = new List<DamageType>();
            LoadEnumListType();
        }

        [ICommand]
        private async Task AddAttack()
        {
            if (string.IsNullOrWhiteSpace(Name) || TypeSelected == DamageType.None)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Type", "Type a name for the damage", "Close");
                return;
            }
            Attack attack = CreateAttack();
            await AttacksDatabase.AddAttack(attack);
            await Shell.Current.GoToAsync("..");
        }

        private void LoadEnumListType()
        {
            foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
                    DamageTypes.Add(damageType);
        }
        private Attack CreateAttack()
        {
            Attack attack = new()
            {
                Name = Name,
                Damage = Damage,
                Type = TypeSelected,
                IsMagic = IsMagic
            };
            return attack;
        }
    }
}
