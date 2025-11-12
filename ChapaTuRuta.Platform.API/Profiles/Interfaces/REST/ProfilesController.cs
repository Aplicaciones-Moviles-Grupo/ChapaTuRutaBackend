using System.Net.Mime;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Profiles")]
public class ProfilesController(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService, ICloudinaryService cloudinaryService):ControllerBase
{
    
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get profile by id", "Get profile by its unique identifier", OperationId = "GetProfileById")]
    [SwaggerResponse(200, "The profile was found and returned", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found")]
    public async Task<IActionResult> GetProfileById(int id)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(id);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if(profile is null) return NotFound();
        var profileResource =  ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [SwaggerOperation("Create profile", "Create a new profile", OperationId = "CreateProfile")]
    [SwaggerResponse(201, "The profile was created", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile was not created")]
    public async Task<IActionResult> CreateProfile([FromForm]CreateProfileResource resource)
    {
        try
        {
            string profileImageUrl = String.Empty;
            string profileImagePublicId = String.Empty;
            if (resource.ProfileImage != null && resource.ProfileImage.Length > 0)
            {
                (profileImageUrl,profileImagePublicId) = await cloudinaryService.UploadImageAsync(resource.ProfileImage, "profiles");
            }
            
            var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource, profileImageUrl, profileImagePublicId);
            var profile = await profileCommandService.Handle(createProfileCommand);
            if (profile is null) return NotFound();
            var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
            return CreatedAtAction(nameof(GetProfileById), new { id = profile.Id }, profileResource);
            
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    
    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [SwaggerOperation("Updates a profile", "Updates a profile", OperationId = "UpdateProfile")]
    [SwaggerResponse(200, "The profile was successfully updated", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile could not be updated")]
    public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileResource resource, int id)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(id);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if(profile is null) return NotFound();
        try
        {
            string profileImageUrl = profile.ProfileImageUrl;
            string profileImagePublicId = profile.ProfileImagePublicId;
            if (resource.ProfileImage != null && resource.ProfileImage.Length > 0)
            {
                string defaultProfileImageUrl = "https://res.cloudinary.com/dtxv5wnbj/image/upload/v1762385933/default-profile_kxt5l2.jpg";
                string defaultProfileImagePublicId = "default-profile_kxt5l2";

                if (profileImagePublicId != defaultProfileImagePublicId && profileImageUrl != defaultProfileImageUrl)
                {
                    await cloudinaryService.DeleteImageAsync(profile.ProfileImagePublicId);    
                }
                
                (profileImageUrl, profileImagePublicId) = await cloudinaryService.UploadImageAsync(resource.ProfileImage, "profiles");
            }
            var updateProfileCommand = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(resource, id,profileImageUrl, profileImagePublicId);
            
            profile = await profileCommandService.Handle(updateProfileCommand);
            if (profile is null) return NotFound();
            var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
            return Ok(profileResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpGet("/drivers")]
    [SwaggerOperation("Get all driver profiles", "Get all driver profiles", OperationId = "GetAllDriverProfiles")]
    [SwaggerResponse(200, "The driver profiles were found")]
    [SwaggerResponse(404, "The driver profiles were not found")]
    public async Task<IActionResult> GetAllDriverProfiles()
    {
        ProfileType type = ProfileType.Driver;
        var getAllDriverProfilesQuery = new GetAllDriverProfilesQuery(type);
        var profiles = await profileQueryService.Handle(getAllDriverProfilesQuery);
        var profilesResource = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(profilesResource);
    }
    
    
}