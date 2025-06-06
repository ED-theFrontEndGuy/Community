using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class TripBLLMapper : IMapper<TripBLLDto, TripDto>
{
    private readonly TripExpenseBLLMapper _tripExpenseMapper = new TripExpenseBLLMapper();

    public TripBLLDto? Map(TripDto? entity)
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
            TripExpenses = entity.TripExpenses?
                .Select(te => _tripExpenseMapper.Map(te))
                .ToList()!
        };

        return res;
    }

    public TripDto? Map(TripBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new TripDto
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