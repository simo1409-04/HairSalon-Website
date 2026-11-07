namespace HairSalon.Data.Contracts
{
    public interface IRepository
    {
        public IQueryable<T> All<T>() where T : class;

        public void Add<T>(T model) where T : class;
        public void Remove<T>(T model) where T : class;
        public void RemoveRange<T>(ICollection<T> models) where T : class;


        public Task<int> SaveChangesAsync();
    }
}
