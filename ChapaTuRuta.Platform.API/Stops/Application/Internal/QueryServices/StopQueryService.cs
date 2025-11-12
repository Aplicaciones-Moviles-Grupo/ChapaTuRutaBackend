using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Stops.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Services;

namespace ChapaTuRuta.Platform.API.Stops.Application.Internal.QueryServices;

public class StopQueryService(IStopRepository stopRepository, IUnitOfWork unitOfWork): IStopQueryService
{
    public async Task<Stop?> Handle(GetStopByIdQuery query)
    {
        return await stopRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Stop>> Handle(GetStopsByDriverIdQuery query)
    {
        return await stopRepository.ListByDriverIdAsync(query.DriverId);
    }
}