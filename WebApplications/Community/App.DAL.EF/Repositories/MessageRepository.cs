using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public override async Task<IEnumerable<Message>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(m => m.Conversation)
            .Include(m => m.User)
            // .Where(m => m.UserId == userId)
            .ToListAsync();
    }

    public override async Task<Message?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(m => m.Conversation)
            .Include(m => m.User)
            .Where(m => m.Id == id && m.UserId == userId)
            .FirstOrDefaultAsync();
    }
}