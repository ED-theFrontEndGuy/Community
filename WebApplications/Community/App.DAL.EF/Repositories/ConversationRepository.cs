using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
{
    public ConversationRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public override async Task<IEnumerable<Conversation>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(c => c.StudyGroup)
            .Where(c => c.StudyGroup!.UserId == userId)
            .ToListAsync();
    }

    public override async Task<Conversation?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(c => c.StudyGroup)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }
}