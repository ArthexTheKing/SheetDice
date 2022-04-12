using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SheetDice.Models;

namespace SheetDice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principale : ContentPage
    {
        public Principale()
        {
            InitializeComponent();
        }

        private static int click = 0;

        private void HdAumento(object sender, EventArgs e)
        {
            click++;
            Hd.Text = $"{click}";
        }

        private void HdMeno(object sender, EventArgs e)
        {
            if (click > 0)
            {
                click -= 1;
                Hd.Text = $"{click}";
            }
        }

        private static int isp = 1;
        private void Ispirazione(object sender, EventArgs e)
        {
            if (isp == 1)
            {
                ((Button)sender).Text = "O";
                isp = 0;
            }
            else
            {
                ((Button)sender).Text = "X";
                isp = 1;
            }
        }

        private async void ListView_Seleziono(object sender, SelectedItemChangedEventArgs e)
        {
            var Attack = ((ListView)sender).SelectedItem as Attack;
            if (Attack == null)
                return;

            await DisplayAlert("Attack Selected", Attack.Name, "Ok");
        }

        private void ListView_Tap(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var Attack = ((MenuItem)sender).BindingContext as Attack;
            if (Attack == null)
                return;

            await DisplayAlert("Attack Favourite", Attack.Name, "Ok");
        }
    }
}