using Business.Constants;
using ServerApi.Contexts;
using ServerApi.DbModels;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ServerApi.Controllers
{
    [Authorize]
    public class MenuController : BaseApiController
    {
        private readonly RestaurantDbContext _dbContext;

        public MenuController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetValidMenuWithDiscount()
        {
            var menu = await _dbContext.DishesAndDrinks
                .Include(x => x.DishAndDrinkCategory)
                .Where(x => x.IsValid)
                .Select(x => new DishAndDrinkMenu
                {
                    CategoryName = x.DishAndDrinkCategory.Name,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price,
                    Id = x.Id,
                })
                .ToListAsync();

            var now = DateTime.UtcNow;
            var discounts = _dbContext.DiscountDishesAndDrinks
                .Where(x => x.DateStart >= now && x.DateEnd <= now_
                .ToList();

            discounts.ForEach(discount =>
            {
                var dishAndDrink = menu.FirstOrDefault(x => x.Id == discount.Id);

                if (dishAndDrink is null) return;

                dishAndDrink.Discount = true;
                dishAndDrink.Price -= dishAndDrink.Price * discount.DiscountValue / 100f;
            });

            return Ok(menu);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMenu()
        {
            var menu = await _dbContext.DishesAndDrinks
                .Include(x => x.DishAndDrinkCategory)
                .Select(x => new DishAndDrinkMenu
                {
                    CategoryName = x.DishAndDrinkCategory.Name,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price,
                    Id = x.Id,
                    IsValid = x.IsValid,
                })
                .ToListAsync();

            var now = DateTime.UtcNow;
            var discounts = await _dbContext.DiscountDishesAndDrinks
                .Where(x => x.DateStart >= now && x.DateEnd <= now)
                .ToListAsync();

            discounts.ForEach(discount =>
            {
                var dishAndDrink = menu.FirstOrDefault(x => x.Id == discount.Id);

                if (dishAndDrink is not null) dishAndDrink.Discount = true;
            });

            return Ok(menu);
        }

        [HttpGet]
        public async Task<IActionResult> GetDishAndDrinkById([FromBody] int id)
        {
            var dishAndDrink = await _dbContext.DishesAndDrinks
                .FirstOrDefaultAsync(x => x.Id == id);

            return dishAndDrink is not null ? 
                Ok(dishAndDrink) : 
                BadRequest("Напиток(блюдо) не найден(o)");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDishAndDrinkByName([FromBody] DishAndDrinkUpdate model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = await _dbContext.DishAndDrinkCategories
                .SingleOrDefaultAsync(x => x.Name == model.CategoryName);

            if (category is null) BadRequest("Категория не найдена");

            var dishAndDrink = await _dbContext.DishesAndDrinks
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (dishAndDrink is null) BadRequest("Напиток(блюдо) не найден(o)");

            dishAndDrink.Description = model.Description;
            dishAndDrink.Name = model.Name;
            dishAndDrink.Price = model.Price;
            dishAndDrink.DishAndDrinkCategoryId = category.Id;
            dishAndDrink.DishAndDrinkCategory = category;
            dishAndDrink.IsValid = model.IsValid;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Administrator)]
        public async Task<IActionResult> AddDishAndDrink([FromBody] DishAndDrinkAdd model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = await _dbContext.DishAndDrinkCategories
                .SingleOrDefaultAsync(x => x.Name == model.CategoryName);

            if (category is null) BadRequest("Категория не найдена");

            var dishAndDrink = new DishAndDrink
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                IsValid = model.IsValid,
                DishAndDrinkCategory = category,
                DishAndDrinkCategoryId = category.Id
            };

            _dbContext.DishesAndDrinks.Add(dishAndDrink);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Administrator)]
        public async Task<IActionResult> AddDishAndDrinkCategory([FromBody] string name)
        {
            if (!string.IsNullOrWhiteSpace(name)) BadRequest($"Название не может быть пустым");

            var category = await _dbContext.DishAndDrinkCategories.SingleOrDefaultAsync(x => x.Name == name);

            if (category is not null) BadRequest("Такая категория уже существует");

            var newCategory = new DishAndDrinkCategory
            {
                Name = name,
            };

            _dbContext.DishAndDrinkCategories.Add(newCategory);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDishAndDrinkCategories()
        {
            var categories = await _dbContext.DishAndDrinkCategories
                .Select(x => x.Name)
                .ToListAsync();

            return Ok(categories);
        }
    }
}
