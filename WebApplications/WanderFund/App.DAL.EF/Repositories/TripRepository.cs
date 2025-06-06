using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TripRepository : BaseRepository<TripDto, Trip>, ITripRepository
{
    public TripRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TripUOWMapper())
    {
    }
}