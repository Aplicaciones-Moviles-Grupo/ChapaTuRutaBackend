using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Routes.Domain.Repositories;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Routes.Application.Internal.CommandServices;

public class StopRouteCommandService(IStopRepository stopRepository, 
    IStopRouteRepository stopRouteRepository, 
    IUnitOfWork unitOfWork): IStopRouteCommandService{
    
    public async Task<StopRoute?> Handle(CreateStopRouteCommand command)
    {
        var stopRoute = new StopRoute(command);
        try
        {
            await stopRouteRepository.AddAsync(stopRoute);
            await unitOfWork.CompleteAsync();
            return stopRoute;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating stop route: {e.Message}");
            return null;
        }
    }

    public async Task Handle(DeleteStopRouteCommand command)
    {
        var stopRoute = await stopRouteRepository.FindByIdAsync(command.Id);
        if (stopRoute == null) throw new Exception("Stop route not found");
        stopRouteRepository.Remove(stopRoute);
        await unitOfWork.CompleteAsync();
    }
}