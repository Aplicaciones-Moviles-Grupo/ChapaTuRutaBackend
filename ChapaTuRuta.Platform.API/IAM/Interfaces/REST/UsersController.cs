using System.Net.Mime;
using ChapaTuRuta.Platform.API.IAM.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.IAM.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.IAM.Domain.Services;
using ChapaTuRuta.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available user endpoints")]
public class UsersController(IUserQueryService userQueryService):ControllerBase
{
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user by its id", Description = "Get user by its id", OperationId = "GetUserById")]
    [SwaggerResponse(200, "The user was found", Type = typeof(UserResource))]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Get all users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
}