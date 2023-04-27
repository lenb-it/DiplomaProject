using ServerApi.Contexts;

namespace ServerApi.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly RestaurantDbContext _dbContext;

        public OrderController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
