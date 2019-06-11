using System.Collections.Generic;
using System.Threading.Tasks;

namespace Resender.Services
{
    public interface IRepository<T> where T : IDataBaseEntity
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }

    public interface IDataBaseEntity
    {
        int Id { get; set; }
    }
}