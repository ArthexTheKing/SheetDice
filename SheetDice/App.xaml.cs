using SQLite;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SheetDice
{
    public partial class App : Application
    {
        public static SQLiteAsyncConnection DataBase { get; private set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            DataBaseConnection();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void DataBaseConnection()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "SheetDice.db");
            DataBase = new SQLiteAsyncConnection(path);
        }

    }
}
