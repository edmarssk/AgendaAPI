using AgendaCOP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Data.Mappings
{
    public class ProcedimentoMapping : IEntityTypeConfiguration<Procedimento>
    {
        public void Configure(EntityTypeBuilder<Procedimento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(100)");

            builder.Property(p => p.Descricao).IsRequired().HasColumnType("varchar(200)");

            builder.Property(p => p.Valor).IsRequired();

            builder.ToTable("Procedimentos");

            builder.HasMany(a => a.Agendas)
                .WithOne(p => p.Procedimento)
                .HasForeignKey(p => p.ProcedimentoId);
        }
    }
}
