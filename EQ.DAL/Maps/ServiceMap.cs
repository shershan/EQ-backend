using EQ.DAL.Models;
using EQ.DAL.MapExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EQ.DAL.Maps
{
    internal class ServiceMap : BaseMap<Service>
    {
        protected internal override string SchemaName
        {
            get => "service";
        }

        protected override void Configure(EntityTypeBuilder<Service> entityBuilder)
        {
            entityBuilder.AddBase();
        }
    }
}