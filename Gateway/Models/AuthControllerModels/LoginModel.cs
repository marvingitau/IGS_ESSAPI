using System.ComponentModel.DataAnnotations;

namespace Gateway.Models.AuthControllerModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
