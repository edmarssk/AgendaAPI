using AgendaCOP.API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.ViewModel
{
    [ModelBinder(typeof(JsonModelBinder), Name = "cliente")]
    public class ClienteImagemVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Nome Obrigatório!")]
        [StringLength(100, ErrorMessage = "O nome deve ser entre {0} e {1} caracteres!", MinimumLength = 5)]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        [Required(ErrorMessage = "Campo E-mail Obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo CPF Obrigatório!")]
        public string Cpf { get; set; }

        public string TelefoneFixo { get; set; }

        public string TelefoneCelular { get; set; }

        [Required(ErrorMessage = "Campo Data de Nascimento Obrigatório!")]
        [DataType(DataType.Date, ErrorMessage = "Campo Data de nascimento inválido!")]
        public DateTime DataNascimento { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Campo Ativo Obrigatório!")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Campo Plano Obrigatório!")]
        public int Plano { get; set; }

        [ScaffoldColumn(false)]
        public string CaminhoFoto { get; set; }

        public IFormFile Imagem { get; set; }

        public string NomeImagem { get; set; }

        //[Display(Name = "Foto Perfil")]
        //public IFormFile CaminhoFotoFormFile { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Campo Data de vencimento inválido!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataVencimentoPlano { get; set; }

        public EnderecoVM Endereco { get; set; }

        public IEnumerable<AgendaVM> Agendas { get; set; }
    }
}
