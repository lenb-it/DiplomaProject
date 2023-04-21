using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class RegisterViewModel : IViewModelIsValid
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PhoneNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public IEnumerable<string> Errors()
        {
            //todo err
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) 
                || string.IsNullOrWhiteSpace(ConfirmPassword) || ConfirmPassword != Password
                || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrWhiteSpace(LastName));
        }
    }
}
