using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
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


        public CharacterSelectionViewModel()
        {
            Characters = new ObservableRangeCollection<Character>
            {
                new Character(){Name = "Lilia"},
                new Character(){Name = "Albert"},
                new Character(){Name = "Baldwin"}
            };


            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Character>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);

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
            await Task.Delay(1000);
            Characters.Clear();
            LoadMore();
            IsBusy = false;
        }

        void LoadMore()
        {
            if (Characters.Count >= 20)
                return;

            Characters.Add(new Character() { Name = "Lilia" });
            Characters.Add(new Character() { Name = "Albert" });
            Characters.Add(new Character() { Name = "Baldwin" });

        }

        void DelayLoadMore()
        {
            if (Characters.Count <= 10)
                return;

            LoadMore();
        }


        void Clear()
        {
            Characters.Clear();
        }
    }
}
