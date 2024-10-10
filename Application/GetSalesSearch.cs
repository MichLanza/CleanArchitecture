using EnterpriseLayer;
using System.Linq.Expressions;

namespace Application
{
    public class GetSalesSearch<TModel>
    {
        private readonly IRepositorySearch<TModel, Sale> _repository;

        public GetSalesSearch(IRepositorySearch<TModel, Sale> repository)
            => _repository = repository;

        public async Task<IEnumerable<Sale>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
            =>  await _repository.GetAsync(predicate);
        

    }
}
