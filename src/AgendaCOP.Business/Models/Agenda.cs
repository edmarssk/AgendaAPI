using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Models
{
    public class Agenda: Entity
    {
        public Guid ClienteId { get; set; }
        public Guid ProcedimentoId { get; set; }
        public DateTime Data { get; set; }
        public bool ManutencaoMensal { get; set; }
        public bool Confirmado { get; set; }
        public bool Cancelado { get; set; }

        //EF Relations
        public Cliente Cliente { get; set; }
        public Procedimento Procedimento { get; set; }
    }
}
