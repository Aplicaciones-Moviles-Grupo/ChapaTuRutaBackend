using System.Net.Mime;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Stops.Domain.Services;
using ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.Stops.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Stops")]
public class StopsController(IStopCommandService stopCommandService, IStopQueryService stopQueryService, ICloudinaryService cloudinaryService):ControllerBase
{

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get stop by id", "Get stop by its unique identifier", OperationId = "GetStopById")]
    [SwaggerResponse(200, "The stop was found and returned", typeof(StopResource))]
    [SwaggerResponse(404, "The stop was not found")]
    public async Task<IActionResult> GetStopById(int id)
    {
        var getStopByIdQuery = new GetStopByIdQuery(id);
        var stop = await stopQueryService.Handle(getStopByIdQuery);
        if(stop == null) return NotFound();
        var stopResource = StopResourceFromEntityAssembler.ToResourceFromEntity(stop);
        return Ok(stopResource);
    }

    [HttpGet]
    [SwaggerOperation("Get stops by driver id", "Get stops by driver id", OperationId = "GetStopsByDriverId")]
    [SwaggerResponse(200, "The stops were found and returned", typeof(StopResource))]
    [SwaggerResponse(404, "The stops were not found")]
    public async Task<IActionResult> GetStopsByDriverId([FromQuery] int driverId)
    {
        var getStopsByDriverIdQuery = new GetStopsByDriverIdQuery(driverId);
        var stops = await stopQueryService.Handle(getStopsByDriverIdQuery);
        var stopsResource = stops.Select(StopResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(stopsResource);
    }
    
    
    [HttpPost]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [SwaggerOperation("Create stop", "Create a new stop", OperationId = "CreateStop")]
    [SwaggerResponse(201, "The stop was created", typeof(StopResource))]
    [SwaggerResponse(400, "The stop was not created")]
    public async Task<IActionResult> CreateStop([FromForm] CreateStopResource resource)
    {
        try
        {
            string stopImageUrl = String.Empty;
            string stopImagePublicId = String.Empty;
            if (resource.StopImage != null && resource.StopImage.Length > 0)
            {
                (stopImageUrl,stopImagePublicId) = await cloudinaryService.UploadImageAsync(resource.StopImage, "stops");
            }
            var createStopCommand = CreateStopCommandFromResourceAssembler.ToCommandFromResource(resource, stopImageUrl, stopImagePublicId);
            var stop = await stopCommandService.Handle(createStopCommand);
            if (stop is null) return NotFound();
            var stopResource = StopResourceFromEntityAssembler.ToResourceFromEntity(stop);
            
            return CreatedAtAction(nameof(GetStopById), new {id = stop.Id}, stopResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [SwaggerOperation("Updates a stop", "Updates a stop", OperationId = "UpdateStop")]
    [SwaggerResponse(200, "The stop was successfully updated", typeof(StopResource))]
    [SwaggerResponse(400, "The stop could not be updated")]
    public async Task<IActionResult> UpdateStop([FromForm] UpdateStopResource resource, int id)
    {
        var getStopByIdQuery = new GetStopByIdQuery(id);
        var stop = await stopQueryService.Handle(getStopByIdQuery);
        if(stop is null) return NotFound();
        try
        {
            string stopImageUrl = stop.StopImageUrl;
            string stopImagePublicId = stop.StopImagePublicId;
            if (resource.StopImage != null && resource.StopImage.Length > 0)
            {
                await cloudinaryService.DeleteImageAsync(stop.StopImagePublicId);
                (stopImageUrl, stopImagePublicId) = await cloudinaryService.UploadImageAsync(resource.StopImage, "stops");
            }
            
            var updateStopCommand = UpdateStopCommandFromResourceAssembler.ToCommandFromResource(resource, id, stopImageUrl, stopImagePublicId);
            stop = await stopCommandService.Handle(updateStopCommand);
            if (stop is null) return NotFound();
            var stopResource = StopResourceFromEntityAssembler.ToResourceFromEntity(stop);
            return Ok(stopResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Deletes stop", Description = "Deletes stop", OperationId = "DeleteStop")]
    [SwaggerResponse(StatusCodes.Status200OK, "The stop resource was successfully deleted", typeof(StopResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Stop not found")]
    public async Task<IActionResult> DeleteStop(int id)
    {
        var deleteStopCommand = new DeleteStopCommand(id);
        await stopCommandService.Handle(deleteStopCommand);
        return Ok("Stop deleted successfully");
    }
    
    
}