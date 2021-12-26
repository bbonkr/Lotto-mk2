using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LottoMk2.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LottoMk2.Data.EntityTypeConfigurations;

public class LottoTypeConfiguration : IEntityTypeConfiguration<Lotto>
{
    public void Configure(EntityTypeBuilder<Lotto> builder)
    {
        builder.ToTable(nameof(Lotto));

        builder.HasKey(x => x.Round);

        builder.Property(x => x.Round)
            .IsRequired();
        builder.Property(x => x.LotteryDate)
            .IsRequired();
        builder.Property(x => x.Num1)
            .IsRequired();
        builder.Property(x => x.Num2)
            .IsRequired();
        builder.Property(x => x.Num3)
            .IsRequired();
        builder.Property(x => x.Num4)
            .IsRequired();
        builder.Property(x => x.Num5)
            .IsRequired();
        builder.Property(x => x.Num6)
            .IsRequired();
        builder.Property(x => x.NumBonus)
            .IsRequired();
    }
}
