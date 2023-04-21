using Business.Contexts;
using Business.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace ServerApi.Controllers
{
    public class MenuController : BaseApiController
    {
        private readonly RestaurantDbContext _dbContext;

        public MenuController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(getMenu)]
        public IActionResult Menu 
    }
}
