using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Models
{
    public class Procedimento: Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        // EF Relation
        public IEnumerable<Agenda> Agendas { get; set; }
    }
}
