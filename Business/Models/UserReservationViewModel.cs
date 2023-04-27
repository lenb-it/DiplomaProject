using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserReservationViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
