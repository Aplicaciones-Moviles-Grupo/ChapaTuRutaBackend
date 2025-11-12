using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChapaTuRuta.Platform.API.Routes.Infrastructure.Persistence.EFC.Configuration.Extensions;


public static class ModelBuilderExtensions
{
    public static void ApplyRoutesConfiguration(this ModelBuilder builder)
    {
        var routeStateConverter = new ValueConverter<RouteState,string>(
            v=> v.ToString(),
            v => Enum.Parse<RouteState>(v));
        
        
        //Routes
        builder.Entity<TravelRoute>().HasKey(s => s.Id);
        builder.Entity<TravelRoute>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TravelRoute>().Property(s => s.Name).IsRequired();
        builder.Entity<TravelRoute>().Property(s => s.Price).IsRequired();
        builder.Entity<TravelRoute>().Property(s => s.Duration).IsRequired();
        builder.Entity<TravelRoute>().Property(s => s.Distance).IsRequired();
        builder.Entity<TravelRoute>().Property(s => s.State).IsRequired().HasConversion(routeStateConverter);
        builder.Entity<TravelRoute>().Property(s => s.PolylineRoute).IsRequired();
        builder.Entity<TravelRoute>().Property(s => s.DriverId).IsRequired();
        
        
        //StopRoutes
        builder.Entity<StopRoute>().HasKey(s => s.Id);
        builder.Entity<StopRoute>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<StopRoute>().Property(s => s.StopId).IsRequired();
        builder.Entity<StopRoute>().Property(s => s.RouteId).IsRequired();
    }
}