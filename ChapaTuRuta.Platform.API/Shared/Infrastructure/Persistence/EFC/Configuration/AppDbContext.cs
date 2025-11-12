using ChapaTuRuta.Platform.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ChapaTuRuta.Platform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ChapaTuRuta.Platform.API.Routes.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ChapaTuRuta.Platform.API.Stops.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options): DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyIamConfiguration();
        builder.ApplyProfilesConfiguration();
        builder.ApplyStopsConfiguration();
        builder.ApplyRoutesConfiguration();
        
        builder.UseSnakeCaseNamingConvention();
    }
}