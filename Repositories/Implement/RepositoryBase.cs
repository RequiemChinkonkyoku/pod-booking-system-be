using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        PodBookingSystemDbContext _context;
        DbSet<T> _dbSet;

        public RepositoryBase()
        {
            _context = new PodBookingSystemDbContext();
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


    }
}
