using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository
{
    public interface IGenerecRepository<T> where T : BaseEntity
    {
        #region Without Specification

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        #endregion Without Specification

        #region With Specification

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);

        Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec);

        #endregion With Specification

        Task AddAsync(T item);

        Task DeleteAsync(T item);
    }
}