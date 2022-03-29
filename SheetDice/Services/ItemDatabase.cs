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

        static async Task init()
        {
            if (db != null)
                return;

            var _dataPath = Path.Combine(FileSystem.AppDataDirectory, "SheetDice.db");

            db = new SQLiteAsyncConnection(_dataPath);

            await db.CreateTableAsync<Item>();
        }

        public static async Task AddItem(Item item)
        {
            await init();
            await db.InsertAsync(item);
        }

        public static async Task RemoveItem(int id)
        {
            await init();
            await db.DeleteAsync<Item>(id);
        }



    }
}
