using SheetDice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SheetDice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterSelection : ContentPage
    {
        public CharacterSelection()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var character = ((ListView)sender).SelectedItem as Character;
            if (character == null) {
                return;
            }
            await DisplayAlert("Character selected", character.Name, "ok");
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var character = ((MenuItem)sender).BindingContext as Character;
            if (character == null)
            {
                return;
            }
            await DisplayAlert("Character favorited", character.Name, "ok");
        }
    }
}