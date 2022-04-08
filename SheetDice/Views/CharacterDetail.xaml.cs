using SheetDice.Services;
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
    [QueryProperty(nameof(CharacterID), nameof(CharacterID))]
    public partial class CharacterDetail : ContentPage
    {
        public string CharacterID { get; set; }
        public CharacterDetail()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(CharacterID, out var result);

            BindingContext = await CharacterDatabase.GetCharacterObject(result);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}