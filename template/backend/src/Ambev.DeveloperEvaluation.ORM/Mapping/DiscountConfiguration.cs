using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.MinQuantity).IsRequired();
        builder.Property(u => u.MaxQuantity).IsRequired();
        builder.Property(u => u.Percentage).IsRequired();
        builder.HasData(
            new Discount { Id = Guid.NewGuid(), MinQuantity = 4, MaxQuantity = 9, Percentage = .1 },
            new Discount { Id = Guid.NewGuid(), MinQuantity = 10, MaxQuantity = 20, Percentage = .2 }
        );
    }
}
