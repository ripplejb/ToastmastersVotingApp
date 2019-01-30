using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;

namespace Voting.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: EntityBase
    {
        #region Private Member Variables

        private readonly VotingContext _votingContext; 

        #endregion
        
        #region Constructors

        public Repository(VotingContext context)
        {
            _votingContext = context;
        }

        #endregion

        #region Public Methods

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = _votingContext.Set<TEntity>().Add(entity).Entity;
            await _votingContext.SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _votingContext.Entry(entity).State = EntityState.Modified;
            await _votingContext.SaveChangesAsync();
            return entity;
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _votingContext.Set<TEntity>().Remove(entity);
            await _votingContext.SaveChangesAsync();
        }
        
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _votingContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _votingContext.Set<TEntity>()
                .Where(predicate)
                .ToListAsync();
        }

        #endregion
    }
}