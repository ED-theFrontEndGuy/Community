using App.DAL.DTO;
using App.Domain;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TimelogMapper : IMapper<TimelogDto, Timelog>
{
    public TimelogDto? Map(Timelog? entity)
    {
        throw new NotImplementedException();
    }

    public Timelog? Map(TimelogDto? entity)
    {
        throw new NotImplementedException();
    }
}