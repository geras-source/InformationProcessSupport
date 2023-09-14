using System.ComponentModel.DataAnnotations;

namespace InformationProcessSupport.Server.Dtos
{
    public class UserAuthenticationDto
    {
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
