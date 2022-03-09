using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.ViewModel
{
    public class EnderecoVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(100, ErrorMessage = "O Logradouro deve ser entre {0} e {1} caracteres!", MinimumLength = 3)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(10, ErrorMessage = "O numero deve ter no máximo 10 caracteres!")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Bairro { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Cep { get; set; }
    }
}
