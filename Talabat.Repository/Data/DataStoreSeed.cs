using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
	public class DataStoreSeed 
	{
		public static async Task SeedAsync (StoreContext dbContext)
		{
			var BrandPath = "../Talabat.Repository/Data/DataSeed/brands.json";
			await TransferData<ProductBrand>(BrandPath, dbContext);

			var TypePath = "../Talabat.Repository/Data/DataSeed/types.json";
			await TransferData<ProductType>(TypePath, dbContext);

			var ProductPath = "../Talabat.Repository/Data/DataSeed/products.json";
			await TransferData<Product>(ProductPath, dbContext);

			var DeliveryPath = "../Talabat.Repository/Data/DataSeed/delivery.json";
			await TransferData<DeliveryMethod>(DeliveryPath, dbContext);


			///if(!dbContext.ProductBrands.Any())
			///{
			///	var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
			///	var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
			///	if(Brands?.Count > 0)
			///	{
			///		foreach(var Brand in Brands)
			///			await dbContext.Set<ProductBrand>().AddAsync(Brand);
			///		await dbContext.SaveChangesAsync();
			///	}
			///}
		}

		private static async Task TransferData <T>(string DataPath, StoreContext dbContext) where T : BaseEntity
		{
			if (!dbContext.Set<T>().Any())
			{
				var ItemsData = File.ReadAllText(DataPath);
				var Items = JsonSerializer.Deserialize<List<T>>(ItemsData);
				if (Items?.Count > 0)
				{
					foreach (var Item in Items)
						await dbContext.Set<T>().AddAsync(Item);
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
