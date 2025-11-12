using System.Security.Authentication;
using ChapaTuRuta.Platform.API.IAM.Application.Internal.OutboundServices;
using ChapaTuRuta.Platform.API.IAM.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.IAM.Domain.Services;
using ChapaTuRuta.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace ChapaTuRuta.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next, ILogger<RequestAuthorizationMiddleware> logger)
{
    private readonly ILogger<RequestAuthorizationMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, IUserQueryService userQueryService, ITokenService tokenService)
    {
        _logger.LogInformation("Entering InvokeAsync");
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!.Metadata
            .Any(m=>m.GetType() == typeof(AllowAnonymousAttribute));
        
        _logger.LogInformation("Allow Anonymous is {AllowAnonymous}", allowAnonymous);
        if (allowAnonymous)
        {
            _logger.LogInformation("Skipping Authorization");
            await next(context);
            return;
        }
        _logger.LogInformation("Entering authorization");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is null) throw new AuthenticationException("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);
        
        if(userId is null) throw new AuthenticationException("Invalid token");

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

        var user = await userQueryService.Handle(getUserByIdQuery);
        _logger.LogInformation("Successful authorization. Updating Context...");
        context.Items["User"] = user;
        _logger.LogInformation("Continuing with Middleware Pipeline");
        await next(context);
    }
}