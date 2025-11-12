using ChapaTuRuta.Platform.API.IAM.Application.Internal.CommandServices;
using ChapaTuRuta.Platform.API.IAM.Application.Internal.OutboundServices;
using ChapaTuRuta.Platform.API.IAM.Application.Internal.OutboundServices.ACL;
using ChapaTuRuta.Platform.API.IAM.Application.Internal.QueryServices;
using ChapaTuRuta.Platform.API.IAM.Domain.Repositories;
using ChapaTuRuta.Platform.API.IAM.Domain.Services;
using ChapaTuRuta.Platform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using ChapaTuRuta.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using ChapaTuRuta.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using ChapaTuRuta.Platform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using ChapaTuRuta.Platform.API.IAM.Infrastructure.Tokens.JWT.Services;
using ChapaTuRuta.Platform.API.IAM.Interfaces.ACL;
using ChapaTuRuta.Platform.API.Profiles.Application.ACL;
using ChapaTuRuta.Platform.API.Profiles.Application.Internal.CommandServices;
using ChapaTuRuta.Platform.API.Profiles.Application.Internal.QueryServices;
using ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;
using ChapaTuRuta.Platform.API.Profiles.Infrastructure.Repositories;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.ACL;
using ChapaTuRuta.Platform.API.Routes.Application.Internal.CommandServices;
using ChapaTuRuta.Platform.API.Routes.Application.Internal.QueryServices;
using ChapaTuRuta.Platform.API.Routes.Domain.Repositories;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Routes.Infrastructure.Repositories;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Cloudinary.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Cloudinary.Services;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ChapaTuRuta.Platform.API.Stops.Application.Internal.CommandServices;
using ChapaTuRuta.Platform.API.Stops.Application.Internal.QueryServices;
using ChapaTuRuta.Platform.API.Stops.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Services;
using ChapaTuRuta.Platform.API.Stops.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConventions()));
builder.Services.AddEndpointsApiExplorer();

//Add CORS Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if(connectionString == null) throw new InvalidOperationException("Connection string not found");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

//Add Swagger/OpenAPI Support
builder.Services.AddSwaggerGen(options =>
{
    //Generar API Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ChapaTuRuta Platform API",
        Version = "v1",
        Description = "ChapaTuRuta Platform API",
        Contact = new OpenApiContact
        {
            Name = "Frock StartUp",
            Email = "contact@frock.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        },
    });
    
    //Ennable Annotations for Swagger
    options.EnableAnnotations();
    
    //Add Bearer Authentitcation for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    // Add Security Requirement for Swagger
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

//Dependency Injection

//Shared

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//IAM Context

//Token Settings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IExternalProfileService, ExternalProfileService>();

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleCommandService, VehicleCommandService>();
builder.Services.AddScoped<IVehicleQueryService, VehicleQueryService>();

builder.Services.AddScoped<IStopRepository, StopRepository>();
builder.Services.AddScoped<IStopCommandService, StopCommandService>();
builder.Services.AddScoped<IStopQueryService, StopQueryService>();

builder.Services.AddScoped<IStopRouteRepository, StopRouteRepository>();
builder.Services.AddScoped<IStopRouteCommandService, StopRouteCommandService>();
builder.Services.AddScoped<IStopRouteQueryService, StopRouteQueryService>();

builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRouteCommandService, TravelRouteCommandService>();
builder.Services.AddScoped<IRouteQueryService, TravelRouteQueryService>();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();


var app = builder.Build();

//Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Use Swagger for API documentation if in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();