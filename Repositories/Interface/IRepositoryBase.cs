using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        void Add(T item);

        public Task AddAsync(T item);

        void Update(T item);

        public Task UpdateAsync(T item);

        void Delete(T item);

        Task<T> FindByIdAsync(int id);
    }
}
