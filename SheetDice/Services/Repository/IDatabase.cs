using System.Collections.Generic;
using System.Threading.Tasks;

namespace SheetDice.Services.Repository
{
    public interface IDatabase<T>
    {
        public Task Insert(T obj);
        public Task Update(T obj);
        public Task Delete(T obj);
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
    }
}
