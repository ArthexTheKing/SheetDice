using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using SQLite;
using SheetDice.Models;

namespace SheetDice.Services
{
    public static class ItemDatabase
    {
        private static SQLiteAsyncConnection db;

        private static async Task Init()
        {
            if (db != null)
                return;

            var _dataPath = Path.Combine(FileSystem.AppDataDirectory, "SheetDice.db");

            db = new SQLiteAsyncConnection(_dataPath);

            await db.CreateTableAsync<Item>();
        }

        public static async Task AddItem(Item item)
        {
            await Init();
            await db.InsertAsync(item);
        }

        public static async Task RemoveItem(int id)
        {
            await Init();
            await db.DeleteAsync<Item>(id);
        }



    }
}
