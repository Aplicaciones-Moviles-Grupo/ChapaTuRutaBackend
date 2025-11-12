using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Stops.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Services;

namespace ChapaTuRuta.Platform.API.Stops.Application.Internal.CommandServices;

public class StopCommandService(IStopRepository stopRepository, IUnitOfWork unitOfWork):IStopCommandService
{
    public async Task<Stop?> Handle(CreateStopCommand command)
    {
        var stop = new Stop(command);
        try
        {
            await stopRepository.AddAsync(stop);
            await unitOfWork.CompleteAsync();
            return stop;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating profile: {e.Message}");
            return null;
        }
    }

    public async Task<Stop?> Handle(UpdateStopCommand command)
    {
        var stop = await stopRepository.FindByIdAsync(command.Id);
        if(stop == null) throw new Exception("Stop not found");
        
        var updatedStop = stop.UpdateInformation(command.Name,command.Address, command.Latitude, command.Longitude, command.StopImageUrl, command.StopImagePublicId);
        stopRepository.Update(updatedStop);
        await unitOfWork.CompleteAsync();
        return updatedStop;
    }

    public async Task Handle(DeleteStopCommand command)
    {
        var stop = await stopRepository.FindByIdAsync(command.Id);
        if(stop == null) throw new Exception("Stop not found");
        stopRepository.Remove(stop);
        await unitOfWork.CompleteAsync();
    }
}