using System.ComponentModel.DataAnnotations;

namespace Business.DbModels
{
    public class DishAndDrinkCategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}