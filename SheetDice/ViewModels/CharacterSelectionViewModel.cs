using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using SheetDice.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;


namespace SheetDice.ViewModels
{
    public class CharacterSelectionViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Character> Characters { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Character> FavoriteCommand { get; }
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
            FavoriteCommand = new AsyncCommand<Character>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Character>(Remove);

        }

        async Task Favorite(Character character)
        {
            if(character == null) {
                return; 
            }
            await Application.Current.MainPage.DisplayAlert("Favorited", character.Name, "OK");
        }

        Character selectedCharacter;

        public Character SelectedCharacterCV
        {
            get=> selectedCharacter;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    value = null;
                }
                selectedCharacter = value;
                OnPropertyChanged();
            }
        }

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
            SelectedCharacter = null;
            await Application.Current.MainPage.DisplayAlert("Selected", character.Name, "OK");
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
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Inserire nome", "Ok");
            await CharacterDatabase.AddCharacter(name);
            await Refresh();
        }

        async Task Remove(Character character)
        {
            await CharacterDatabase.RemoveCharacter(character.Id);
            await Refresh();
        }
    }
}
