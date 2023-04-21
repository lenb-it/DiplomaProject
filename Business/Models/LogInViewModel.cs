using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class LogInViewModel : IViewModelIsValid
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<string> Errors()
        {
            //todo err
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password));
        }
    }
}
