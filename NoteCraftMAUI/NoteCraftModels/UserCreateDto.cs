using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftModels
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Username must be provided")]
        [MinLength(2)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email must be provided")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
