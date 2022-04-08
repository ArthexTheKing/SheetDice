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

        public static async Task AddCharacter(string name, string classe, string level, string alignement, string race, string background, string subclass, string subrace)
        {
            await Init();
            var character = new Character
            {
                Name = name,
                Classe = classe,
                Level = level,
                Alignment = alignement,
                Race = race,
                Background = background,
                Subclass = subclass,
                Subrace = subrace
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

        public static async Task<Character> GetCharacterObject(int id)
        {
            await Init();

            var character = await db.Table<Character>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return character;
        }
    }
}
