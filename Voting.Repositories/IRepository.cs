using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T t);
        Task<T> UpdateAsync(T t);
        Task RemoveAsync(T t);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
    }
}