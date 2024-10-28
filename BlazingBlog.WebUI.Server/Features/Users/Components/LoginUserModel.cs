using System.ComponentModel.DataAnnotations;

namespace BlazingBlog.WebUI.Server.Features.Users.Component
{
    public class LoginUserModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}
