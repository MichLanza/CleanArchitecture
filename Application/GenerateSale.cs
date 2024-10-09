using Application.Exceptions;
using EnterpriseLayer;

namespace Application
{
    public class GenerateSale<TDTO>
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IMapper<TDTO, Sale> _mapper;

        public GenerateSale(IRepository<Sale> saleRepository, IMapper<TDTO, Sale> mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO saleDTO)
        {
            var sale = _mapper.Map(saleDTO);

            if (sale.Concepts.Count == 0)
            {
                throw new ValidationException("Una venta debe tener conceptos");
            }

            if (sale.Total <= 0)
            {
                throw new ValidationException("una venta debe tener más de $ 0.00 en total");
            }

            await _saleRepository.AddAsync(sale);
        }

    }
}
