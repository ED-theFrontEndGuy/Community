using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserTripRepository : BaseRepository<UserTripDto, UserTrip>, IUserTripRepository
{
    public UserTripRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserTripUOWMapper())
    {
    }
}