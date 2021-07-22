using EQ.DAL.MapExtensions;
using EQ.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EQ.DAL.Maps
{
    internal class UserMap : BaseMap<User>
    {
        protected internal override string SchemaName
        {
            get => "identity";
        }

        protected override void Configure(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.AddBase();

            entityBuilder
                .HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);
        }
    }
}