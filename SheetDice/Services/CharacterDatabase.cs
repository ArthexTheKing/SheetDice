using SheetDice.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SheetDice.Services
{
    public static class CharacterDatabase
    {
        private static SQLiteAsyncConnection db;

        private static async Task Init()
        {
            if (db != null)
                return;


            var _dataPath = Path.Combine(FileSystem.AppDataDirectory, "SheetDice.db");

            db = new SQLiteAsyncConnection(_dataPath);

            await db.CreateTableAsync<Character>();
        }

        public static async Task AddCharacter(string name)
        {
            await Init();
            var character = new Character
            {
                Name = name
            };
            await db.InsertAsync(character);
        }

        public static async Task RemoveCharacter(int id)
        {
            await Init();
            await db.DeleteAsync<Character>(id);
        }

        public static async Task<IEnumerable<Character>> GetCharacter()
        {
            await Init();

            var character = await db.Table<Character>().ToListAsync();
            return character;
        }
    }
}
