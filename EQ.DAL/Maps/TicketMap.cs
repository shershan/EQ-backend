using EQ.DAL.MapExtensions;
using EQ.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EQ.DAL.Maps
{
    internal class TicketMap : BaseMap<Ticket>
    {
        protected internal override string SchemaName
        {
            get => "service";
        }

        protected override void Configure(EntityTypeBuilder<Ticket> entityBuilder)
        {
            entityBuilder.AddBase();

            entityBuilder
                .HasOne(x => x.Window)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.WindowId)
                .IsRequired(false);

            entityBuilder.HasOne(x => x.Service)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.ServiceId);
        }
    }
}