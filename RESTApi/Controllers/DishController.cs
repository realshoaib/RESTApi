using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RESTApi.Models;
using RESTApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpGet]
        public async Task<List<Dish>> GetDishes()
        {
            var availableDishes = await _dishService.GetDishesAsync();
            return availableDishes.FindAll(d => d.IsAvailable == true);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDishById(int id)
        {
            var dish = await _dishService.GetDishAsync(id);
            
            if (dish is null) return NotFound();

            return dish;
        }
        [HttpPost]
        public async Task CreateDish(Dish newDish)
        {
            await _dishService.CreateDishAsync(newDish);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDish(int id, Dish updateDish)
        {
            var dish = await _dishService.GetDishAsync(id);
            
            if (dish is null) return NotFound();

            updateDish.Id = dish.Id;

            await _dishService.UpdateDishAsync(id, updateDish);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var dish = await _dishService.GetDishAsync(id);

            if (dish is null) return NotFound();

            await _dishService.DeleteDishAsync(id);

            return NoContent();
        }
    }
}
