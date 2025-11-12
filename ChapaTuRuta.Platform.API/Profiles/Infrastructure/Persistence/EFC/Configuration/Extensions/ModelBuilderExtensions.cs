using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChapaTuRuta.Platform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        var profileTypeConverter = new ValueConverter<ProfileType,string>(
            v=> v.ToString(),
            v => Enum.Parse<ProfileType>(v));
        
        //Profile Context
        
        //Profile
        
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().Property(p => p.FirstName).IsRequired();
        builder.Entity<Profile>().Property(p => p.LastName).IsRequired();
        builder.Entity<Profile>().Property(p => p.Email).IsRequired();
        builder.Entity<Profile>().Property(p => p.PhoneNumber).IsRequired();
        builder.Entity<Profile>().Property(p => p.ProfileImageUrl);
        builder.Entity<Profile>().Property(p => p.ProfileImagePublicId);
        builder.Entity<Profile>().Property(p => p.Type).IsRequired().HasConversion(profileTypeConverter);
        builder.Entity<Profile>().Property(p => p.UserId).IsRequired();
        
        //Vehicle
        builder.Entity<Vehicle>().HasKey(v => v.Id);
        builder.Entity<Vehicle>().Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(v => v.Model).IsRequired();
        builder.Entity<Vehicle>().Property(v => v.Plate).IsRequired();
        builder.Entity<Vehicle>().Property(v => v.Color).IsRequired();
        builder.Entity<Vehicle>().Property(v => v.VehicleImageUrl);
        builder.Entity<Vehicle>().Property(v => v.VehicleImagePublicId);
        builder.Entity<Vehicle>().Property(v => v.ProfileId).IsRequired();

    }
}