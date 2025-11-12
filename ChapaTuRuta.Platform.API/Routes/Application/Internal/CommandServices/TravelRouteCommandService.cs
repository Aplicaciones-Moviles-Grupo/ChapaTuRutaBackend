using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Routes.Domain.Repositories;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Routes.Application.Internal.CommandServices;

public class TravelRouteCommandService(IRouteRepository routeRepository, IUnitOfWork unitOfWork): IRouteCommandService
{
    public async Task<TravelRoute?> Handle(CreateRouteCommand command)
    {
        var route = new TravelRoute(command);
        try
        {
            await routeRepository.AddAsync(route);
            await unitOfWork.CompleteAsync();
            return route;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating route: {e.Message}");
            return null;
        }
    }

    public async Task Handle(DeleteRouteCommand command)
    {
        var route = await routeRepository.FindByIdAsync(command.Id);
        if (route == null) throw new Exception("Route not found");
        routeRepository.Remove(route);
        await unitOfWork.CompleteAsync();
    }

    public async Task<TravelRoute?> Handle(ActiveRouteCommand command)
    {
        var route =  await routeRepository.FindByIdAsync(command.Id);
        if (route == null) throw new Exception("Route not found");
        route.Activate();
        routeRepository.Update(route);
        await unitOfWork.CompleteAsync();
        return route;
    }
    
    public async Task<TravelRoute?> Handle(InactiveRouteCommand command)
    {
        var route =  await routeRepository.FindByIdAsync(command.Id);
        if (route == null) throw new Exception("Route not found");
        route.Desactivate();
        routeRepository.Update(route);
        await unitOfWork.CompleteAsync();
        return route;
    }
}