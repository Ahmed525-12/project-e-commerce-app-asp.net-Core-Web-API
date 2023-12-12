using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Repository;

namespace Talabat.Core
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		Task<int> CompleteAsync(); 
		IGenerecRepository<T> Repository<T>() where T : BaseEntity;
	}
}
