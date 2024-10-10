using EnterpriseLayer;

namespace Application
{
    public class GetSales
    {
        private readonly IRepository<Sale> _saleRepository;

        public GetSales(IRepository<Sale> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> ExecuteAsync() => await _saleRepository.GetAllAsync();
    }
}
