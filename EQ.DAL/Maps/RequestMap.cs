using EQ.DAL.MapExtensions;
using EQ.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EQ.DAL.Maps
{
    internal class RequestMap : BaseMap<Request>
    {
        protected internal override string SchemaName
        {
            get => "service";
        }

        protected override void Configure(EntityTypeBuilder<Request> entityBuilder)
        {
            entityBuilder.AddBase();

            entityBuilder
                .HasOne(x => x.Window)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.WindowId)
                .IsRequired(false);
        }
    }
}