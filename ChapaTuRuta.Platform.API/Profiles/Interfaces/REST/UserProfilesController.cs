using System.Net.Mime;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/users/{userId:int}/profile")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Users")]
public class UserProfilesController(IProfileQueryService profileQueryService):ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get profile by user id", "Get profile by user id", OperationId = "GetProfileByUserId")]
    [SwaggerResponse(200, "Returns profile by user id",typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> GetProfileByUserId(int userId)
    {
        var getProfileByUserId = new GetProfileByUserIdQuery(userId);
        var profile = await profileQueryService.Handle(getProfileByUserId);
        if(profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
}