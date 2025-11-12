using System.Net.Mime;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Transform;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Stops.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST;

[ApiController]
[Route("api/v1/routes/{routeId:int}/stops")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Routes")]
public class StopRoutesController(IStopQueryService stopQueryService,
    IRouteQueryService routeQueryService,
    IStopRouteCommandService stopRouteCommandService,
    IStopRouteQueryService stopRouteQueryService):ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Stop Route by id", "Get Stop Route by its unique identifier",
        OperationId = "GetStopRouteById")]
    [SwaggerResponse(200, "The Stop Route was found and returned", typeof(StopRouteResource))]
    [SwaggerResponse(404, "The Stop Route was not found")]
    public async Task<IActionResult> GetStopRouteById([FromRoute] int routeId,[FromRoute] int id)
    {
        var getRouteByIdQuery = new GetRouteByIdQuery(routeId);
        var route = await routeQueryService.Handle(getRouteByIdQuery);
        if (route is null) return NotFound("Route not found or doesn't exist");
        var getStopRouteByIdQuery = new GetStopRouteByIdQuery(id);
        var stopRoute = await stopRouteQueryService.Handle(getStopRouteByIdQuery);
        if (stopRoute is null) return NotFound("Stop Route not found or doesn't exist");
        var stopRouteResource = StopRouteResourceFromEntityAssembler.ToResourceFromEntity(stopRoute);
        return Ok(stopRouteResource);
    }

    [HttpGet()]
    [SwaggerOperation("Get Stops by Route Id", "Get Stops Route by route Id",
        OperationId = "GetStopRoutesByRouteId")]
    [SwaggerResponse(200, "The Stop Route was found and returned", typeof(IEnumerable<StopRouteResource>))]
    [SwaggerResponse(404, "The Stop Route was not found")]
    public async Task<IActionResult> GetStopRoutesByRouteId([FromRoute] int routeId)
    {
        var getStopRoutesByRouteIdQuery = new GetStopRoutesByRouteIdQuery(routeId);
        var stopRoutes = await stopRouteQueryService.Handle(getStopRoutesByRouteIdQuery);

        foreach (var stopRoute in stopRoutes)
        {
            var getStopByIdQuery = new GetStopByIdQuery(stopRoute.StopId);
            var stop = await stopQueryService.Handle(getStopByIdQuery);
            stopRoute.Stop = stop;
        }
        var stopRouteResources = stopRoutes.Select(StopRouteResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(stopRouteResources);
    }
    
    [HttpPost]
    [SwaggerOperation("Create Stop Route", "Create a StopRoute", OperationId = "CreateStopRoute")]
    [SwaggerResponse(201, "The Stop Route was created", typeof(StopRouteResource))]
    [SwaggerResponse(400, "The vehicle was not created")]
    public async Task<IActionResult> CreateStopRoute([FromBody] CreateStopRouteResource resource,
        [FromRoute] int routeId)
    {
        var stopId = resource.StopId;
        var getStopByIdQuery = new GetStopByIdQuery(stopId);
        var stop = await stopQueryService.Handle(getStopByIdQuery);
        if (stop is null) return NotFound("Stop not found or doesn't exist");
        var createStopRouteCommand = CreateStopRouteCommandFromResourceAssembler.ToCommandFromResource(resource, routeId, stop);
        var stopRoute = await stopRouteCommandService.Handle(createStopRouteCommand);
        if (stopRoute is null) return BadRequest();
        var stopRouteResource = StopRouteResourceFromEntityAssembler.ToResourceFromEntity(stopRoute);
        return CreatedAtAction(nameof(GetStopRouteById), new { id = stopRoute.Id , routeId = routeId}, stopRouteResource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Deletes Stop Route", Description = "Deletes Stop Route",
        OperationId = "DeleteStopRoute")]
    [SwaggerResponse(StatusCodes.Status200OK, "The stop route resource was successfully deleted",
        typeof(StopRouteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Stop route not found")]
    public async Task<IActionResult> DeleteStop([FromRoute] int routeId,[FromRoute] int id)
    {
        var getRouteByIdQuery = new GetRouteByIdQuery(routeId);
        var route = await routeQueryService.Handle(getRouteByIdQuery);
        if (route is null) return NotFound("Route not found or doesn't exist");
        var deleteStopRouteCommand = new DeleteStopRouteCommand(id);
        await stopRouteCommandService.Handle(deleteStopRouteCommand);
        return Ok("Stop Route deleted successfully");
    }
    
    
}