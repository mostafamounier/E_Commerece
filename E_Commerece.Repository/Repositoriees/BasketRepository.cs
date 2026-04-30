using E_Commerece.Core.Models;
using E_Commerece.Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerece.Repository.Repositoriees
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _DataBase;

        public BasketRepository(IConnectionMultiplexer Redis)
        {
            this._DataBase = Redis.GetDatabase();
        }
        public Task<bool> DeleteBasket(string id)
        {
            return _DataBase.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasket(string id)
        {
            var basket =await _DataBase.StringGetAsync(id);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
            var createdorupdatedbasket = await _DataBase.StringSetAsync(basket.Id,JsonSerializer.Serialize<CustomerBasket>(basket),TimeSpan.FromDays(1));
            return await GetBasket(basket.Id);
        }
    }
}
