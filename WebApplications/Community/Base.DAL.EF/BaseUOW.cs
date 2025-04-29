using Base.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUOW<TDbContext> : IBaseUOW
    where TDbContext : DbContext
{
    protected readonly TDbContext UOWDbContext;

    public BaseUOW(TDbContext context)
    {
        UOWDbContext = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await UOWDbContext.SaveChangesAsync();
    }
}