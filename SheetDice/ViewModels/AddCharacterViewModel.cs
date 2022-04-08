using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SheetDice.ViewModels
{
    public class AddCharacterViewModel : BaseViewModel
    {
        string name, classe, level, alignement, race, background, subclass, subrace;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Classe { get => classe; set => SetProperty(ref classe, value); }
        public string Level { get => level; set => SetProperty(ref level, value); }
        public string Alignement { get => alignement; set => SetProperty(ref alignement, value); }
        public string Race { get => race; set => SetProperty(ref race, value); }
        public string Background { get => background; set => SetProperty(ref background, value); }
        public string Subclass { get => subclass; set => SetProperty(ref subclass, value); }
        public string Subrace { get => subrace; set => SetProperty(ref subrace, value); }

        public AsyncCommand SaveCommand { get; }

        public AddCharacterViewModel()
        {
            Title = "Add character";
            SaveCommand = new AsyncCommand(Save);
        }

        async Task Save()
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(classe) || string.IsNullOrEmpty(level) || string.IsNullOrEmpty(alignement) || string.IsNullOrEmpty(race))
            {
                return;
            }
            await CharacterDatabase.AddCharacter(name, classe, level, alignement, race, background, subclass, subrace);

            await AppShell.Current.GoToAsync("..");
        } 

    }
}
