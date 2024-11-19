using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace GRPC.DB.Configurations
{
    public class TestDbEntitiesConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("Workers", "public");
            builder.HasKey(x => x.ID);
        }
    }
}
