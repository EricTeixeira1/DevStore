using System;
using DevStore.SharedKernel.Domain;

namespace DevStore.SharedKernel.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}