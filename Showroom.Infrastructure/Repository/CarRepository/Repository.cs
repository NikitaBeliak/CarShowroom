﻿using Microsoft.EntityFrameworkCore;
using Showroom.Domain;
using Showroom.Domain.Entitis.CarEntities.Interface;

namespace Showroom.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(Context context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public async Task Delete(TEntity entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll() => await _entities.ToListAsync();

        public async Task<TEntity> GetById(int id) => await _entities.FindAsync(id);

        public async Task Insert(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
