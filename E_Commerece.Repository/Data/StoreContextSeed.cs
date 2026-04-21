using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerece.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brandsdata = File.ReadAllText("../E_Commerece.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<Core.Models.ProductBrand>>(brandsdata);

                foreach (var item in brands)
                {
                    await context.ProductBrands.AddAsync(item);
                }

                await context.SaveChangesAsync(); // ✅ مهم
            }
            if (!context.ProductTypes.Any())
            {
                var typesdata = File.ReadAllText("../E_Commerece.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<Core.Models.ProductType>>(typesdata);

                foreach (var item in types)
                {
                    await context.ProductTypes.AddAsync(item);
                }

                await context.SaveChangesAsync(); // ✅ مهم
            }

            if (!context.Products.Any())
            {
                var productsdata = File.ReadAllText("../E_Commerece.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Core.Models.Product>>(productsdata);

                foreach (var item in products)
                {
                    await context.Products.AddAsync(item);
                }

                await context.SaveChangesAsync(); // ✅ مهم
            }


        }
    }
}
