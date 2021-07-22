using EQ.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EQ.DAL.MapExtensions
{
    public static class BaseMapExtension
    {
        public static EntityTypeBuilder<TEntity> AddBase<TEntity>(this EntityTypeBuilder<TEntity> map)
            where TEntity : class, IBaseEntity
        {
            map.HasKey(x => x.Id);
            map.Property(x => x.Id).ValueGeneratedOnAdd();

            return map;
        }
    }
}
