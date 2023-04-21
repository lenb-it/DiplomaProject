using System.ComponentModel.DataAnnotations;

namespace Business.DbModels
{
    public class DishAndDrink
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int CategoryID { get; set; }

        public float Price { get; set; }

        public string ImageString64 { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public bool IsValid { get; set; }

        public DishAndDrinkCategory DishAndDrinkCategory { get; set; }
    }
}
