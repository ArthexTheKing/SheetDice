using MvvmHelpers;
using MvvmHelpers.Commands;
using SheetDice.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheetDice.ViewModels
{
    public class CharacterSelectionViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Character> Characters { get; }
        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Character> FavoriteCommand { get; } 
        
        public CharacterSelectionViewModel()
        {
            Characters = new ObservableRangeCollection<Character>
            {
                new Character("Lilia"),
                new Character("Albert"),
                new Character("Baldwin")
            };

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Character>(Favorite);

        }

        async Task Favorite(Character character)
        {
            if(character == null) {
                return; 
            }
            await Application.Current.MainPage.DisplayAlert("Favorited", character.Name, "OK");
        } 

        Character previouslySelected;
        Character selectedCharacter;
        public Character SelectedCharacter
        {
            get=> selectedCharacter;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    previouslySelected = value;
                    value = null;
                }
                selectedCharacter = value;
                OnPropertyChanged();
            }
        }


        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}
