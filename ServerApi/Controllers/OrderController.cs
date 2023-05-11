using System.Xml;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApi.Contexts;

namespace ServerApi.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly RestaurantDbContext _dbContext;

        public OrderController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var orders = await _dbContext.Orders
                .Take(500)
                .Include(x => x.User)
                .Select(x => new Business.Models.Order
                {
                    Id = x.Id,
                    IsPaid = x.IsPaid,
                    NumberPlace = x.NumberPlace,
                    Price = x.Price,
                    Email = x.User.Email,
                    Name = $"{x.User.FirstName} {x.User.LastName}",
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetLastFiveHundred()
        {
            var orders = await _dbContext.Orders
                .Take(500)
                .Include(x => x.User)
                .Select(x => new Business.Models.Order
                {
                    Id = x.Id,
                    IsPaid = x.IsPaid,
                    NumberPlace = x.NumberPlace,
                    Price = x.Price,
                    Email = x.User.Email,
                    Name = $"{x.User.FirstName} {x.User.LastName}",
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Business.Models.Order model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (order is null) return BadRequest("Заказ не найден");

            order.Price = model.Price;
            order.NumberPlace = model.NumberPlace;
            order.IsPaid = model.IsPaid;
            order.ChangeAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order is null) return BadRequest("Заказ не найден");

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
