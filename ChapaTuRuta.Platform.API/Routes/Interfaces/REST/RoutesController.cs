using System.Net.Mime;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Routes")]
public class RoutesController(IRouteCommandService routeCommandService, IRouteQueryService routeQueryService):ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get route by id", "Get route by its unique identifier", OperationId = "GetRouteById")]
    [SwaggerResponse(200, "The route was found and returned", typeof(TravelRouteResource))]
    [SwaggerResponse(404, "The route was not found")]
    public async Task<IActionResult> GetRouteById([FromRoute]int id)
    {
        var getRouteByIdQuery = new GetRouteByIdQuery(id);
        var route = await routeQueryService.Handle(getRouteByIdQuery);
        if(route is null) return NotFound();
        var routeResource =  TravelRouteResourceFromEntityAssembler.ToResourceFromEntity(route);
        return Ok(routeResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Route", "Create a new route", OperationId = "CreateRoute")]
    [SwaggerResponse(201, "The route was created", typeof(TravelRouteResource))]
    [SwaggerResponse(400, "The route was not created")]
    public async Task<IActionResult> CreateRoute([FromBody] CreateRouteResource resource)
    {
        var createRouteCommand = CreateRouteCommandFromResourceAssembler.ToCommandFromResource(resource);
        var route = await routeCommandService.Handle(createRouteCommand);
        if (route is null) return BadRequest();
        var routeResource = TravelRouteResourceFromEntityAssembler.ToResourceFromEntity(route);
        return CreatedAtAction(nameof(GetRouteById), new { id = route.Id }, routeResource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Deletes route", Description = "Deletes route", OperationId = "DeleteRoute")]
    [SwaggerResponse(StatusCodes.Status200OK, "The route resource was successfully deleted",
        typeof(TravelRouteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Stop not found")]
    public async Task<IActionResult> DeleteRoute(int id)
    {
        var deleteRouteCommand = new DeleteRouteCommand(id);
        await routeCommandService.Handle(deleteRouteCommand);
        return Ok("Route deleted successfully");
    }

    [HttpPost("{routeId:int}/active")]
    [SwaggerOperation(Summary = "Activate a route", Description = "Activate a route", OperationId = "ActiveRoute")]
    [SwaggerResponse(StatusCodes.Status200OK, "The route resource was successfully Activated",
        typeof(TravelRouteResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Route not found")]
    public async Task<IActionResult> ActivateRoute([FromRoute] int routeId)
    {
        var activateRouteCommand = new ActiveRouteCommand(routeId);
        var route = await routeCommandService.Handle(activateRouteCommand);
        if (route is null) return NotFound("Route not found or doesn't exist");
        var routeResource = TravelRouteResourceFromEntityAssembler.ToResourceFromEntity(route);
        return Ok(routeResource);
    }

    [HttpPost("{routeId:int}/inactive")]
    [SwaggerOperation(Summary = "Deactivate a route", Description = "Desactivate a route",
        OperationId = "InactiveRoute")]
    [SwaggerResponse(StatusCodes.Status200OK, "The route resource was successfully desactivated",
        typeof(TravelRouteResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Route not found")]
    public async Task<IActionResult> DeactivateRoute([FromRoute] int routeId)
    {
        var deactivateRouteCommand = new InactiveRouteCommand(routeId);
        var route = await routeCommandService.Handle(deactivateRouteCommand);
        if (route is null) return NotFound("Route not found or doesn't exist");
        var routeResource = TravelRouteResourceFromEntityAssembler.ToResourceFromEntity(route);
        return Ok(routeResource);
    }
}