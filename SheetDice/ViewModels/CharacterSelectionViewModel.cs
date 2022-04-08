using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using SheetDice.Services;
using SheetDice.Views;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;


namespace SheetDice.ViewModels
{
    public class CharacterSelectionViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Character> Characters { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Character> RemoveCommand { get; }



        public CharacterSelectionViewModel()
        {
            Characters = new ObservableRangeCollection<Character>();

            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Character>(Remove);

        }

        Character selectedCharacter;

        public Character SelectedCharacter
        {
            get => selectedCharacter;
            set => SetProperty(ref selectedCharacter, value);
        }

        async Task Selected(object args)
        {
            var character = args as Character;
            if (character == null)
            {
                return;
            }
            var route = $"{nameof(CharacterDetail)}?CharacterID={character.Id}";
            await Shell.Current.GoToAsync(route);
            SelectedCharacter = null;
        }


        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            var characters = await CharacterDatabase.GetCharacter();
            Characters.Clear();
            Characters.AddRange(characters);
            IsBusy = false;
        }

        async Task Add()
        {
            var route = $"{nameof(CharacterCreation)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Character character)
        {
            await CharacterDatabase.RemoveCharacter(character.Id);
            await Refresh();
        }
    }
}
