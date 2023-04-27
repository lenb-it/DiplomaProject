using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class DishAndDrinkMenu
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string CategoryName { get; set; }

        public float Price { get; set; }

        public string ImageString64 { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }
    }
}
