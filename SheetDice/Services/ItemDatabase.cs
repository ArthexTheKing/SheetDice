using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using SQLite;
using SheetDice.Models;
using System.Collections.Generic;

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

        public static async Task UpdateItem(Item item)
        {
            await Init();
            await db.UpdateAsync(item);
        }

        public static async Task RemoveItem(int id)
        {
            await Init();
            await db.DeleteAsync<Item>(id);
        }

        public static async Task<IEnumerable<Item>> GetItems()
        {
            await Init();
            var items = await db.Table<Item>().ToListAsync();
            return items;
        }

        public static async Task<Item> GetItem(int id)
        {
            await Init();
            var item = await db.GetAsync<Item>(id);
            return item;
        }

    }
}
