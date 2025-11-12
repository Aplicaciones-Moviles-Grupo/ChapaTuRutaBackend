using ChapaTuRuta.Platform.API.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        //IAM Context
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Email).IsRequired();
        builder.Entity<User>().Property(u=> u.PasswordHash).IsRequired();
    }
}