using System.ComponentModel.DataAnnotations;

namespace HobbyHub.Models
{
    public class LoginUser
    {
        [Required]
        [Display(Name="Username: ")]
        public string LoginUsername { get; set; }
        [Required]
        [Display(Name="Password: ")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}