using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.Stops.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyStopsConfiguration(this ModelBuilder builder)
    {
        
        //Stops
        builder.Entity<Stop>().HasKey(s => s.Id);
        builder.Entity<Stop>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Stop>().Property(s => s.Name).IsRequired();
        builder.Entity<Stop>().Property(s => s.Address).IsRequired();
        builder.Entity<Stop>().Property(s => s.Latitude).IsRequired();
        builder.Entity<Stop>().Property(s => s.Longitude).IsRequired();
        builder.Entity<Stop>().Property(s => s.StopImageUrl);
        builder.Entity<Stop>().Property(s => s.StopImagePublicId);
        builder.Entity<Stop>().Property(s => s.DriverId).IsRequired();
    }
}