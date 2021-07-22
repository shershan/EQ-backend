using EQ.DAL.MapExtensions;
using EQ.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EQ.DAL.Maps
{
    internal class RoleMap : BaseMap<Role>
    {
        protected internal override string SchemaName
        {
            get => "identity";
        }

        protected override void Configure(EntityTypeBuilder<Role> entityBuilder)
        {
            entityBuilder.AddBase();
        }
    }
}
