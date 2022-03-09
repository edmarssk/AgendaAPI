using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Models
{
    public class Endereco: Entity
    {
        public Guid ClienteId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        //EF Relation
        public Cliente Cliente { get; set; }
    }
}
