using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.ViewModel
{
    public class UserVM
    {
        [Required(ErrorMessage ="Campo Email obrigatorio!")]
        [EmailAddress(ErrorMessage ="Campo Email inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Password obrigatorio!")]
        [StringLength(100, ErrorMessage = "Campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo ConfirmPassword obrigatorio!")]
        [Compare("Password", ErrorMessage ="Campo confirmação diferente do Password")]
        public string ConfirmPassword { get; set; }
    }
}
