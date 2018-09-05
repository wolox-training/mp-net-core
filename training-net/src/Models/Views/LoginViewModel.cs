using System.ComponentModel.DataAnnotations;

namespace training_net.Models.Views
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
