using HairSalon.Data.Contracts;

namespace HairSalon.Data;

public class Repository(HairSalonDb context) : IRepository
{
    public IQueryable<T> All<T>() where T : class
    {
        return context.Set<T>();
    }

    public void Add<T>(T model) where T : class
    {
        context.Set<T>().Add(model);
    }
    public void Remove<T>(T model) where T : class
    {
        context.Set<T>().Remove(model);
    }
    public void RemoveRange<T>(ICollection<T> models) where T : class
    {
        context.Set<T>().RemoveRange(models);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
