namespace Base.DAL.Interfaces;

public interface IBaseUOW
{
    public Task<int> SaveChangesAsync();
}