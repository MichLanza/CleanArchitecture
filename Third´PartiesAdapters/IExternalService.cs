namespace ThirdPartiesAdapters
{
    public interface IExternalService <T>
    {
        public Task<IEnumerable<T>> GetContentAsync();
    }
}
