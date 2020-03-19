using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OKEX.Auto.Core.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.ORM.EntityConfigurations
{
    public class BaseCurrencyConfiguration: IEntityTypeConfiguration<BaseCurrency>
    {
        public void Configure(EntityTypeBuilder<BaseCurrency> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("BaseCurrency");

            entityTypeBuilder.HasKey(ct => ct.Id);
            entityTypeBuilder.Property(ct => ct.Id).ValueGeneratedNever();
            entityTypeBuilder.Property(ct => ct.Status).IsRequired();

            entityTypeBuilder.Property(ct => ct.currency).IsRequired().HasMaxLength(256);
            entityTypeBuilder.Property(ct => ct.name).HasMaxLength(256);
            entityTypeBuilder.Property(ct => ct.can_deposit).HasMaxLength(256);
            entityTypeBuilder.Property(ct => ct.can_withdraw).HasMaxLength(256);
            entityTypeBuilder.Property(ct => ct.min_withdrawal).HasMaxLength(256);
        }
    }
}
