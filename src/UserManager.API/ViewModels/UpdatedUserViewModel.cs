using System.ComponentModel.DataAnnotations;

namespace UserManager.API.ViewModels
{
    public class UpdatedUserViewModel
    {
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "O Id não pode ser menor que 1")]
        public long Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "O {0} deve ter pelo menos 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O {0} deve ter no máximo 80 caracteres.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MinLength(10, ErrorMessage = "O {0} deve ter pelo menos 10 caracteres.")]
        [MaxLength(100, ErrorMessage = "O {0} deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "O {0} deve ter pelo menos 6 caracteres.")]
        [MaxLength(100, ErrorMessage = "O {0} deve ter no máximo 80 caracteres.")]
        public string Password { get; set; }
    }
}
