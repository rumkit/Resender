using System;

namespace Resender.Services
{
    public interface IDataService
    {
        IRepository<T> GetRepository<T>() where T : IDataBaseEntity, new();
    }
}
