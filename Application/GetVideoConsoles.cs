namespace Application
{
    public class GetVideoConsoles<TEntity, TOutput> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IPresenter<TEntity, TOutput> _presenter;
        public GetVideoConsoles(
            IRepository<TEntity> repository,
            IPresenter<TEntity, TOutput> presenter)
        {
            _repository = repository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var consoles =  await _repository.GetAllAsync();

            return _presenter.Present(consoles);
        }
    }
}
