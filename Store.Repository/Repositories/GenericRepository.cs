using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
             => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            =>  _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
             => await _context.Set<TEntity>().AsNoTracking().ToListAsync();


        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
             => await _context.Set<TEntity>().ToListAsync();

        

        //public async Task<TEntity> GetByIdAsNoTrackingAsync(Tkey? id)
        //     => await _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x=> x.Id == id);


        public async Task<TEntity> GetByIdAsync(Tkey? id)
            => await _context.Set<TEntity>().FindAsync(id);

        
        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).ToListAsync();

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).FirstOrDefaultAsync();

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specs)
            => SpecificationEvaluator<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), specs)
    }
}
