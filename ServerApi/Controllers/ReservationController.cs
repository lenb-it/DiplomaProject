using ServerApi.Contexts;
using ServerApi.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Business.Models;
using Microsoft.AspNetCore.Identity;
using Business.Constants;

namespace ServerApi.Controllers
{
    //[Authorize]
    public class ReservationController : BaseApiController
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public ReservationController(RestaurantDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPut]
        //[Authorize(Roles = RoleNames.Worker)]
        public async Task<IActionResult> ConfirmReservation([FromBody] UserReservationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return BadRequest("Пользователь не найден");

            var reservation = await _dbContext.UserReservations
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.Date == model.Date);

            if (reservation is null) return BadRequest("Резервирование не найдено");

            reservation.IsValid = true;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserReservationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return BadRequest("Пользователь не найден");

            var reservation = await _dbContext.UserReservations
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.Date == model.Date);

            if (reservation is null) return BadRequest("Резервирование не найдено");

            _dbContext.UserReservations.Remove(reservation);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Add([FromBody] UserReservationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return BadRequest("Пользователь не найден");

            var reservation = new UserReservation
            {
                UserId = user.Id,
                User = user,
                IsValid = false,
                Date = model.Date,
                CreateAt = DateTime.UtcNow,
                ChangeAt = DateTime.UtcNow,
            };

            _dbContext.UserReservations.Add(reservation);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _dbContext.UserReservations
                .Include(x => x.User)
                .Select(x => new UserReservationViewModel
                {
                    Date = x.Date,
                    Email = x.User.Email,
                })
                .ToListAsync();

            return Ok(reservations);
        }
    }
}
