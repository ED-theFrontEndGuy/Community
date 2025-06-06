using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class TripUOWMapper : IMapper<TripDto, Trip>
{
    private readonly TripExpenseUOWMapper _tripExpenseMapper = new TripExpenseUOWMapper();

    public TripDto? Map(Trip? entity)
    {
        if (entity == null) return null;

        var res = new TripDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Destination = entity.Destination,
            Budget = entity.Budget,
            DepartureDate = entity.DepartureDate,
            ReturnDate = entity.ReturnDate,
            IsPublic = entity.IsPublic,
            TripExpenses = entity.TripExpenses?
                .Select(te => _tripExpenseMapper.Map(te))
                .ToList()!
        };

        return res;
    }

    public Trip? Map(TripDto? entity)
    {
        if (entity == null) return null;

        var res = new Trip()
        {
            Id = entity.Id,
            Name = entity.Name,
            Destination = entity.Destination,
            Budget = entity.Budget,
            DepartureDate = entity.DepartureDate,
            ReturnDate = entity.ReturnDate,
            IsPublic = entity.IsPublic,
        };

        return res;
    }
}