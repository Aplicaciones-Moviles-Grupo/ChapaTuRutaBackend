using System.Net.Mime;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST;

[ApiController]
[Route("api/v1/drivers/{driverId:int}/routes")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Drivers")]

public class DriverRoutesController(IRouteQueryService routeQueryService):ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get routes by driverId", "Get routes by profile Id", OperationId = "GetRoutesByDriverId")]
    [SwaggerResponse(200, "Returns routes by DriverId",typeof(IEnumerable<TravelRouteResource>))]
    [SwaggerResponse(404, "Routes not found")]
    public async Task<IActionResult> GetRoutesByDriverId([FromRoute]int driverId)
    {
        var getRoutesByDriverIdQuery = new GetRoutesByDriverIdQuery(driverId);
        var routes = await routeQueryService.Handle(getRoutesByDriverIdQuery);
        var routesResource = routes.Select(TravelRouteResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(routesResource);
    }
}