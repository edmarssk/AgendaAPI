using AgendaCOP.Business.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Models
{

    public class Cliente: Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public TipoPlano Plano { get; set; }
        public string CaminhoFoto { get; set; }
        public DateTime DataVencimentoPlano { get; set; }
        public Endereco Endereco { get; set; }
      
        //EF Relation
        public IEnumerable<Agenda> Agendas { get; set; }
    }
}
