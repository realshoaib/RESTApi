using RESTApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public interface IDishService
    {
        Task CreateDishAsync(Dish newDish);
        Task<List<Dish>> GetDishesAsync();
        Task<Dish> GetDishAsync(int id);
        Task UpdateDishAsync(int id, Dish updatedDish);
        Task DeleteDishAsync(int id);
    }
}
