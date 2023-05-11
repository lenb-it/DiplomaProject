namespace Business.Models
{
    public class AddOrder
    {
        public int NumberTable { get; set; }

        public string Email { get; set; }

        public List<DishAndDrinkOrder> DishAndDrinks { get; set; }
    }
}
