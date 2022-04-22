using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SheetDice.Services.Repository
{
    public class Sqlite<T> : IDatabase<T> where T : new()
    {
        private SQLiteAsyncConnection DataBase { get; set; }

        public Sqlite()
        {
            DataBase = App.DataBase;
            DataBase.CreateTableAsync<T>();
        }

        public async Task Insert(T obj)
        {
            await DataBase.InsertAsync(obj);
        }

        public async Task Update(T obj)
        {
            await DataBase.UpdateAsync(obj);
        }

        public async Task Delete(T obj)
        {
            await DataBase.DeleteAsync(obj);
        }

        public async Task<T> Get(int id)
        {
            return await DataBase.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await DataBase.Table<T>().ToListAsync();
        }
    }
}
