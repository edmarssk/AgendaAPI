using AgendaCOP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Data.Mappings
{
    public class AgendaMapping : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Data).IsRequired();

            builder.Property(p => p.ManutencaoMensal).IsRequired();

            builder.ToTable("Agendas");
        }
    }
}
