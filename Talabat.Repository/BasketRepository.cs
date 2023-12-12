using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Repository;

namespace Talabat.Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase database;

		public BasketRepository(IConnectionMultiplexer redis)
		{
			database = redis.GetDatabase();
		}



		public Task<bool> DeleteBasketAsync(string id)
		{
			return database.KeyDeleteAsync(id);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string id)
		{
			var basket = await database.StringGetAsync(id);

			return basket.IsNull ? null: JsonSerializer.Deserialize<CustomerBasket>(basket!);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
		{
			var JsonBasket = JsonSerializer.Serialize(Basket);
			var result = await database.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(2));

			return result ? await GetBasketAsync(Basket.Id) : null;
		}
	}
}
