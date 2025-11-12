using System.Net.Mime;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/drivers/{profileId:int}/vehicle")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Drivers")]
public class VehicleProfilesController(IVehicleCommandService vehicleCommandService, 
    IVehicleQueryService vehicleQueryService, 
    ICloudinaryService cloudinaryService,
    IProfileQueryService profileQueryService):ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get vehicle by id", "Get vehicle by id", OperationId = "GetVehicleById")]
    [SwaggerResponse(200, "Returns vehicle by its id",typeof(VehicleResource))]
    [SwaggerResponse(404, "Vehicle not found")]
    public async Task<IActionResult> GetVehicleById([FromRoute]int profileId,[FromRoute]int id)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if(profile == null) return NotFound("Profile not found or doesn't exist");
        var getVehicleByIdQuery = new GetVehicleByIdQuery(id);
        var vehicle = await vehicleQueryService.Handle(getVehicleByIdQuery);
        if(vehicle == null) return NotFound("Vehicle not found or doesn't exist");
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(vehicleResource);
    }
    
    [HttpGet]
    [SwaggerOperation("Get vehicle by ProfileId", "Get vehicle by profile Id", OperationId = "GetVehicleByProfileId")]
    [SwaggerResponse(200, "Returns vehicle by profile id",typeof(VehicleResource))]
    [SwaggerResponse(404, "Vehicle not found")]
    public async Task<IActionResult> GetVehicleByProfileId([FromRoute]int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if(profile == null) return NotFound("Profile not found or doesn't exist");
        var getVehicleByProfileIdQuery = new GetVehicleByProfileIdQuery(profileId);
        var vehicle = await vehicleQueryService.Handle(getVehicleByProfileIdQuery);
        if(vehicle == null) return NotFound("Vehicle not found or doesn't exist");
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(vehicleResource);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [SwaggerOperation("Create vehicle", "Create a new vehicle", OperationId = "CreateVehicle")]
    [SwaggerResponse(201, "The vehicle was created", typeof(VehicleResource))]
    [SwaggerResponse(400, "The vehicle was not created")]
    public async Task<IActionResult> CreateVehicle([FromForm] CreateVehicleResource resource, [FromRoute] int profileId)
    {
        try
        {
            string vehicleImageUrl = String.Empty;
            string vehicleImagePublicId = String.Empty;
            if (resource.VehicleImage != null && resource.VehicleImage.Length > 0)
            {
                (vehicleImageUrl,vehicleImagePublicId) = await cloudinaryService.UploadImageAsync(resource.VehicleImage, "vehicles");
            }
            var createVehicleCommand = CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(resource, vehicleImageUrl, vehicleImagePublicId,profileId);
            var vehicle = await vehicleCommandService.Handle(createVehicleCommand);
            if(vehicle == null) return NotFound("Vehicle not found or doesn't exist");
            var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
            return CreatedAtAction(nameof(GetVehicleById), new {profileId = vehicle.ProfileId, id = vehicle.Id}, vehicleResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [SwaggerOperation("Update vehicle", "Update vehicle", OperationId = "UpdateVehicle")]
    [SwaggerResponse(201, "The vehicle was successfully updated", typeof(VehicleResource))]
    [SwaggerResponse(400, "The vehicle could not be updated")]
    public async Task<IActionResult> UpdateVehicle([FromRoute] int id, [FromRoute] int profileId, [FromForm] UpdateVehicleResource resource)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if(profile == null) return NotFound("Profile not found or doesn't exist");
        var getVehicleByIdQuery = new GetVehicleByIdQuery(id);
        var vehicle = await vehicleQueryService.Handle(getVehicleByIdQuery);
        if(vehicle == null) return NotFound("Vehicle not found or doesn't exist");
        try
        {
            string vehicleImageUrl = vehicle.VehicleImageUrl;
            string vehicleImagePublicId = vehicle.VehicleImagePublicId;
            if (resource.VehicleImage != null && resource.VehicleImage.Length > 0)
            {
                await cloudinaryService.DeleteImageAsync(vehicle.VehicleImagePublicId);
                (vehicleImageUrl,vehicleImagePublicId) = await cloudinaryService.UploadImageAsync(resource.VehicleImage, "vehicles");
            }
            var updateVehicleCommand = UpdateVehicleCommandFromResourceAssembler.ToCommandFromResource(resource,id, vehicleImageUrl, vehicleImagePublicId);
            
            vehicle = await vehicleCommandService.Handle(updateVehicleCommand);
            if(vehicle is null) return NotFound("Vehicle not found or doesn't exist");
            var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
            return Ok(vehicleResource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}