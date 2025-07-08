using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Injections

        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;
        protected virtual IQueryable<T> LimitedQuery => _entities.AsQueryable();

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        #endregion

        #region Add Async

        public async Task AddAsync(T entity) => await _entities.AddAsync(entity);

        #endregion

        #region Remove

        public void Remove(T entity) => _entities.Remove(entity);

        #endregion

        #region Update

        public void Update(T entity) => _entities.Update(entity);

        #endregion

        #region Save Async

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Get By Id Async

        public async Task<T> GetByIdAsync(Guid id)
        {
            var obj = await LimitedQuery.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
            return obj!;
        }

        #endregion

        #region Get Queryable

        public IQueryable<T> GetQueryable()
        {
            var query = _context.Set<T>().AsQueryable();
            return query;
        }

        #endregion
        
    }
