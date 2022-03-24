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
    public partial class Page1 : ContentPage
    {
        public Page1()
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
    }
}