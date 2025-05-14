using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RoomRepository : BaseRepository<RoomDto, Room>, IRoomRepository
{
    public RoomRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new RoomMapper())
    {
    }
}