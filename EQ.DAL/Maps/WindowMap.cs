using EQ.DAL.MapExtensions;
using EQ.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EQ.DAL.Maps
{
    internal class WindowMap : BaseMap<Window>
    {
        protected internal override string SchemaName
        {
            get => "service";
        }

        protected override void Configure(EntityTypeBuilder<Window> entityBuilder)
        {
            entityBuilder.AddBase();

            entityBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.Windows)
                .HasForeignKey(x => x.UserId);

            entityBuilder
                .HasOne(x => x.Service)
                .WithMany(x => x.Windows)
                .HasForeignKey(x => x.ServiceId);
        }
    }
}