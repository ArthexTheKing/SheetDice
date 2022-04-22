using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SheetDice.Services.Repository
{
    public class LocalDB<T> : IDatabase<T> where T : new()
    {
        private readonly SQLiteAsyncConnection dataBase;

        public LocalDB()
        {
            dataBase = App.DataBase;
            dataBase.CreateTableAsync<T>();
        }

        public async Task Insert(T obj)
        {
            await dataBase.InsertAsync(obj);
        }

        public async Task Update(T obj)
        {
            await dataBase.UpdateAsync(obj);
        }

        public async Task Delete(T obj)
        {
            await dataBase.DeleteAsync(obj);
        }

        public async Task<T> Get(int id)
        {
            return await dataBase.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dataBase.Table<T>().ToListAsync();
        }
    }
}
