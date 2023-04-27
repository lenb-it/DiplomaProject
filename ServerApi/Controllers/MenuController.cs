using Business.Constants;
using ServerApi.Contexts;
using ServerApi.DbModels;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace ServerApi.Controllers
{
    //[Authorize]
    public class MenuController : BaseApiController
    {
        private readonly RestaurantDbContext _dbContext;

        public MenuController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenu()
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
                })
                //.GroupBy(x => x.CategoryName)
                .ToListAsync();

            return Ok(menu);
        }

        [HttpGet]
        public async Task<IActionResult> GetDishAndDrinkByName([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Название не может быть пустым");

            var dishAndDrink = await _dbContext.DishesAndDrinks
                .FirstOrDefaultAsync(x => x.Name == name);

            return dishAndDrink is not null ? 
                Ok(dishAndDrink) : 
                BadRequest("Напиток(блюдо) не найден(o)");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDishAndDrinkByName([FromBody] DishAndDrinkUpdateViewModel model)
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
            dishAndDrink.CategoryId = category.Id;
            dishAndDrink.DishAndDrinkCategory = category;
            dishAndDrink.IsValid = model.IsValid;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        //[Authorize(Roles = RoleNames.Administrator)]
        public async Task<IActionResult> AddDishAndDrink([FromBody] DishAndDrinkAddViewModel model)
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
                CategoryId = category.Id
            };

            _dbContext.DishesAndDrinks.Add(dishAndDrink);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        //[Authorize(Roles = RoleNames.Administrator)]
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
        //[Authorize]
        public async Task<IActionResult> GetDishAndDrinkCategories()
        {
            var categories = await _dbContext.DishAndDrinkCategories
                .Select(x => x.Name)
                .ToListAsync();

            return Ok(categories);
        }
    }
}
