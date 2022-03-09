using AgendaCOP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(80)");

            builder.Property(p => p.Email).IsRequired().HasColumnType("varchar(100)");

            builder.Property(p => p.Cpf).IsRequired().HasColumnType("varchar(11)");

            builder.Property(p => p.DataCadastro).IsRequired();

            builder.Property(p => p.DataNascimento).IsRequired();

            builder.Property(p => p.Ativo).IsRequired();

            builder.ToTable("Clientes");

            builder.HasOne(f => f.Endereco)
                .WithOne(e => e.Cliente);

            builder.HasMany(a => a.Agendas)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId);
                
        }
    }
}
