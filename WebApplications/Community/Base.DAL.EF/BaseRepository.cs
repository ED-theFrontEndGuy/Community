

using Base.DAL.Interfaces;
using Base.Domain;
using Base.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseRepository<TEntity> : BaseRepository<TEntity, Guid>, IRepository<TEntity>
    where TEntity : BaseEntity
{
    public BaseRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}

public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> 
    where TEntity : BaseEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    
    protected DbContext RepositoryDbContext;
    protected DbSet<TEntity> RepositoryDbSet;
    

    public BaseRepository(DbContext repositoryDbContext)
    {
        RepositoryDbContext = repositoryDbContext;
        RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
    }

    private IQueryable<TEntity> GetQuery(TKey? userId = default!)
    {
        var query = RepositoryDbSet.AsQueryable();
        
        // todo check userId for null/default
        if (typeof(IDomainUser<TKey, IdentityUser<TKey>>).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => ((IDomainUser<TKey, IdentityUser<TKey>>)e).UserId.Equals(userId));
        }

        return query;
    }
    
    
    public IEnumerable<TEntity> All(TKey? userId)
    {
        return GetQuery(userId)
            .ToList(); 
    }

    public async Task<IEnumerable<TEntity>> AllAsync(TKey? userId = default!)
    {
        return await GetQuery(userId)
            .ToListAsync(); 
    }

    public TEntity? Find(TKey id, TKey? userId)
    {
        var query = GetQuery(userId);
        
        return query.FirstOrDefault(e => e.Id.Equals(id));
    }

    public async Task<TEntity?> FindAsync(TKey id, TKey? userId)
    {
        var query = GetQuery(userId);
        
        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public void Add(TEntity entity)
    {
        RepositoryDbSet.Add(entity);
    }

    public TEntity Update(TEntity entity)
    {
        return RepositoryDbSet.Update(entity).Entity;
    }

    public void Remove(TEntity entity, TKey? userId)
    {
        throw new NotImplementedException();
    }

    public void Remove(TKey id, TKey? userId)
    {
        throw new NotImplementedException();
    }
    
    public void RemoveAsync(TKey id, TKey? userId)
    {
        throw new NotImplementedException();
    }

}