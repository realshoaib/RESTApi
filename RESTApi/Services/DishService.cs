using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RESTApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public class DishService : IDishService
    {
        private readonly IMongoCollection<Dish> _dishes;
        private readonly DishDatabaseSetting dbSetting;

        public DishService(IOptions<DishDatabaseSetting> databaseSettings)
        {
            dbSetting = databaseSettings.Value;

            var client = new MongoClient(dbSetting.ConnectionString);
            var database = client.GetDatabase(dbSetting.DatabaseName);
            _dishes = database.GetCollection<Dish>(dbSetting.DishesCollectionName);
        }

        public async Task CreateDishAsync(Dish newDish)
        {
            await _dishes.InsertOneAsync(newDish);
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await _dishes.Find(dish => true).ToListAsync();
        }

        public async Task<Dish> GetDishAsync(int id)
        {
            return await _dishes.Find<Dish>(dish => dish.id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateDishAsync(int id, Dish updatedDish)
        {
            await _dishes.ReplaceOneAsync(dish => dish.id == id, updatedDish);
        }

        public async Task DeleteDishAsync(int id)
        {
            await _dishes.DeleteOneAsync(dish => dish.id == id);
        }
    }
}
