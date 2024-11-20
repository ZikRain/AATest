using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace GRPC.DB.Configurations
{
    public class TestDbEntitiesConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("workers", "public");
            builder.Property(x => x.ID).HasColumnName("id");
            builder.Property(x => x.FirstName).HasColumnName("firstname");
            builder.Property(x => x.LastName).HasColumnName("lastname");
            builder.Property(x => x.MiddleName).HasColumnName("middlename");
            builder.Property(x => x.Sex).HasColumnName("sex");
            builder.Property(x => x.Birthday).HasColumnName("birthday");
            builder.Property(x => x.HaveChildren).HasColumnName("havechildren");



            builder.HasKey(x => x.ID);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.MiddleName).IsRequired();
            builder.Property(x => x.Birthday).IsRequired();
        }
    }
}
