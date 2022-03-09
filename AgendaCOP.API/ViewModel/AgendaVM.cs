using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.ViewModel
{
    public class AgendaVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Cliente Obrigatório!")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Campo Procedimento Obrigatório!")]
        public Guid ProcedimentoId { get; set; }

        public string ProcedimentoDescricao { get; set; }

        [Required(ErrorMessage = "Campo Data Obrigatório!")]
        [DataType(DataType.Date, ErrorMessage = "Campo Data inválido!")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo Hora Obrigatório!")]
        public string Hora { get; set; }

        [Required(ErrorMessage = "Campo Manutenção Mensal Obrigatório!")]
        public bool ManutencaoMensal { get; set; }

        public bool Confirmado { get; set; }

        public bool Cancelado { get; set; }

        ////EF Relations
        //public ClienteVM Cliente { get; set; }

        //public ProcedimentoVM Procedimento { get; set; }

        ////Carregar na View
        //public List<ProcedimentoVM> ProcedimentosVM { get; set; }

        //public List<ClienteVM> ClientesVM { get; set; }

        //public List<HoraVM> HorasVM { get; set; }

        public AgendaVM()
        {
            //this.ClientesVM = new List<ClienteVM>();
            //this.ProcedimentosVM = new List<ProcedimentoVM>();
            //HorasVM.Append(new HoraVM() { Id = "08:00", Descricao = "08:00" });
            //HorasVM.Append(new HoraVM() { Id = "08:30", Descricao = "08:30" });
            //HorasVM.Append(new HoraVM() { Id = "09:00", Descricao = "09:00" });
            //HorasVM.Append(new HoraVM() { Id = "09:30", Descricao = "09:30" });
            //HorasVM.Append(new HoraVM() { Id = "10:00", Descricao = "10:00" });
            //HorasVM.Append(new HoraVM() { Id = "10:30", Descricao = "10:30" });
            //HorasVM.Append(new HoraVM() { Id = "11:00", Descricao = "11:00" });
            //HorasVM.Append(new HoraVM() { Id = "11:30", Descricao = "11:30" });
            //HorasVM.Append(new HoraVM() { Id = "12:00", Descricao = "12:00" });
            //HorasVM.Append(new HoraVM() { Id = "12:30", Descricao = "12:30" });
            //HorasVM.Append(new HoraVM() { Id = "13:00", Descricao = "13:00" });
            //HorasVM.Append(new HoraVM() { Id = "13:30", Descricao = "13:30" });
            //HorasVM.Append(new HoraVM() { Id = "14:00", Descricao = "14:00" });
            //HorasVM.Append(new HoraVM() { Id = "14:30", Descricao = "14:30" });
            //HorasVM.Append(new HoraVM() { Id = "15:00", Descricao = "15:00" });
            //HorasVM.Append(new HoraVM() { Id = "15:30", Descricao = "15:30" });
            //HorasVM.Append(new HoraVM() { Id = "16:00", Descricao = "16:00" });
            //HorasVM.Append(new HoraVM() { Id = "16:30", Descricao = "16:30" });
            //HorasVM.Append(new HoraVM() { Id = "17:00", Descricao = "17:00" });
            //HorasVM.Append(new HoraVM() { Id = "17:30", Descricao = "17:30" });
            //HorasVM.Append(new HoraVM() { Id = "18:00", Descricao = "18:00" });

        }
    }
}
