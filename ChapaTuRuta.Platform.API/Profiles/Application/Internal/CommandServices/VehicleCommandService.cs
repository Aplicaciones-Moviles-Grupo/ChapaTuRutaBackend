using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Profiles.Application.Internal.CommandServices;

public class VehicleCommandService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork):IVehicleCommandService
{
    public async Task<Vehicle?> Handle(CreateVehicleCommand command)
    {
        var vehicle = new Vehicle(command);
        try
        {
            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.CompleteAsync();
            return vehicle;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating vehicle: {e.Message}");
            return null;
        }
    }

    public async Task<Vehicle?> Handle(UpdateVehicleCommand command)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(command.Id);
        if(vehicle == null) throw new Exception("Vehicle not found");
        
        var updatedVehicle = vehicle.UpdateInformation(command.Model, command.Plate, command.Color, command.VehicleImageUrl,command.VehicleImagePublicId);
        
        vehicleRepository.Update(updatedVehicle);
        await unitOfWork.CompleteAsync();
        return updatedVehicle;
    }
}