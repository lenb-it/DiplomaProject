using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Contexts;
using ServerApi.DbModels;

namespace ServerApi.Controllers
{
    [Authorize]
    public class DiscountController : BaseApiController
    {
        private readonly RestaurantDbContext _dbContext;

        public DiscountController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRange([FromBody] DiscountAddRange model)
        {
            var utcNow = DateTime.UtcNow;
            var discounts = model.DishAndDrinkIds
                .Select(id => new DiscountDishAndDrink
                {
                    ChangeAt = utcNow,
                    CreateAt = utcNow,
                    DateEnd = model.DateEnd,
                    DateStart = model.DateStart,
                    DiscountValue = model.Discount,
                    DishAndDrinkId = id
                })
                .ToArray();

            await _dbContext.AddRangeAsync(discounts);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = _dbContext
                .DiscountDishesAndDrinks
                .ToList();

            return Ok(discounts);
        }
    }
}
