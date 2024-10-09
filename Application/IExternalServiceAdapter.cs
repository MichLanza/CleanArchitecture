namespace Application
{
    public interface IExternalServiceAdapter<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync();    
    }
}
