using System.ComponentModel.DataAnnotations;

namespace hostinpanda.web.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ErrorString { get; set; }
    }
}