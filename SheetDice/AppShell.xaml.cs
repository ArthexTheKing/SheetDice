using SheetDice.Views;
using Xamarin.Forms;

namespace SheetDice
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CharacterCreation), typeof(CharacterCreation));
            Routing.RegisterRoute(nameof(CharacterDetail), typeof(CharacterDetail));
        }
    }
}
