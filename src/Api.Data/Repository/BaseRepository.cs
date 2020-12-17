using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Data;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext database;
        private DbSet<T> _dataSet;
        public BaseRepository(ApplicationDbContext database)
        {
            this.database = database;
            _dataSet = database.Set<T>();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataSet.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if(item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.Now;
                _dataSet.Add(item);

                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if(result == null)
                {
                    return null;
                }

                item.UpdateAt = DateTime.Now;
                item.CreateAt = result.CreateAt;

                database.Entry(result).CurrentValues.SetValues(item);
                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if(result == null)
                {
                    return false;
                }

                _dataSet.Remove(result);
                await database.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}