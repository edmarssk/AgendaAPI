using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.ViewModel
{
    public class ProcedimentoVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Nome Obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo {0} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descrição Obrigatório!")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo {0} caracteres!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo valor Obrigatório!")]
        public decimal Valor { get; set; }
    }
}
