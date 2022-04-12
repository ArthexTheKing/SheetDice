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
    public static class AttacksDatabase
    {
        private static SQLiteAsyncConnection db;
        private static async Task Init()
        {
            if (db != null)
                return;

            var _dataPath = Path.Combine(FileSystem.AppDataDirectory, "SheetDice.db");

            db = new SQLiteAsyncConnection(_dataPath);

            await db.CreateTableAsync<Attack>();
        }

        public static async Task AddAttack(string name, string damage, string type)
        {
            await Init();
            var attack = new Attack
            {
                Name = name,
                Damage = damage,
                Type = type
            };

            var id = await db.InsertAsync(attack);
        }

        public static async Task RemoveAttack(int id)
        {
            await Init();

            await db.DeleteAsync<Attack>(id);
        }

        public static async Task<IEnumerable<Attack>> GetAttack()
        {
            await Init();
            var attack = await db.Table<Attack>().ToListAsync();
            return attack;
        }
    }
}
