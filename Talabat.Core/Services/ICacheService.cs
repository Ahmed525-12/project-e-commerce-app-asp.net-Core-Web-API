using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
	public interface ICacheService
	{
		// Cache Data
		public Task CacheDataAsync(string Key, object Value, TimeSpan ExpireTime);

		// Get Cached Data 
		public Task<string?> GetCachedAsync(string Key);
	}
}
