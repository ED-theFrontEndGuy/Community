using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class TimelogBLLMapper : IBLLMapper<TimelogBLLDto, TimelogDto>
{
    public TimelogBLLDto? Map(TimelogDto? entity)
    {
        throw new NotImplementedException();
    }

    public TimelogDto? Map(TimelogBLLDto? entity)
    {
        throw new NotImplementedException();
    }
}