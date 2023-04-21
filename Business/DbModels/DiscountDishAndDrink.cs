namespace Business.DbModels
{
    public class DiscountDishAndDrink
    {
        public int Id { get; set; }
        
        public int DishAndDrinkId { get; set; }

        public float DiscountValue { get; set; }

        //todo: тип дата
        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public DateTime ChangeAt { get; set; }

        public DateTime CreateAt { get; set; }

        public DishAndDrink DishAndDrink { get; set; }
    }
}
