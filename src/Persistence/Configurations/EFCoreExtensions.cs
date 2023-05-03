using Atos.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public static class EFCoreExtensions
    {
        public static void ConfigurationBase<TKey, TUserKey, TEntity>(this EntityTypeBuilder<TEntity> builder, string tableName, bool userRequired = true, int maxLenghtUser = 256) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            builder.ToTable(tableName);
            builder.Property((TEntity x) => x.Id).ValueGeneratedOnAdd().HasDefaultValue("NEWID()");
            builder.Property((TEntity x) => x.UserCreatorId).HasMaxLength(maxLenghtUser).IsRequired(userRequired);
            builder.Property((TEntity x) => x.State).IsRequired();
            builder.Property((TEntity x) => x.CreatedDate).IsRequired();
        }
    }
}