using VA.Identity.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VA.Identity.Infrastructure.Persistence.Configurations
{
    public abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public virtual string SchemaName { get; } = "public";

        public abstract string TableName { get; }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName, SchemaName);

            ConfigureEntity(builder);

            builder.Property(e => e.Created)
               .IsRequired();

            builder.Property(e => e.CreatedBy)
                .IsRequired();

        }
    }
}