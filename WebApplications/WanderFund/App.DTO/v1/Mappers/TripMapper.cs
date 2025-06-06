using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class TripMapper : IMapper<Trip, TripBLLDto>
{
    private readonly TripExpenseMapper _tripExpenseMapper = new TripExpenseMapper();

    public Trip? Map(TripBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new Trip
        {
            Id = entity.Id,
            Name = entity.Name,
            Destination = entity.Destination,
            Budget = entity.Budget,
            DepartureDate = entity.DepartureDate,
            ReturnDate = entity.ReturnDate,
            IsPublic = entity.IsPublic,
            // TripExpensesTotal = entity.TripExpenses?.Select(te => _tripExpenseMapper.Map(te)).ToList()
        };


        return res;
    }

    public TripBLLDto? Map(Trip? entity)
    {
        if (entity == null) return null;

        var res = new TripBLLDto
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
    
    
    public TripBLLDto Map(TripCreate entity)
    {
        var res = new TripBLLDto
        {
            Id = Guid.NewGuid(),
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