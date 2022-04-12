
using SheetDice.Views;
using SheetDice.Views.Inventario;


using Xamarin.Forms;

namespace SheetDice
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(ItemCreationPage), typeof(ItemCreationPage));
            Routing.RegisterRoute(nameof(ItemModifyPage), typeof(ItemModifyPage));
            Routing.RegisterRoute(nameof(CharacterCreation), typeof(CharacterCreation));
            Routing.RegisterRoute(nameof(CharacterDetail), typeof(CharacterDetail));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));

        }
    }
}
