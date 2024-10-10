
using Application;
using DataAdapters;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using ModelAdapters;

namespace RepositoryAdapters
{
    public class SaleRepository : IRepository<Sale>
    {
        private readonly AppDbContext _appDbContext;

        public SaleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Sale entity)
        {
            var saleModel = new SaleModel
            {
                Total = entity.Total,
                CreationDate = entity.Date,
                Concepts = entity.Concepts.Select(c => new ConceptModel()
                {
                    UnitPrice = c.UnitPrice,
                    IdConsole = c.IdConsole,
                    Quantity = c.Quantity,

                }).ToList()
            };
            await _appDbContext.Sales.AddAsync(saleModel);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _appDbContext.Sales.Select(s =>
                 new Sale(s.CreationDate,
                 _appDbContext.Concepts.Where(c => c.IdSale == s.Id).Select(c => new Concept(c.Quantity, c.UnitPrice, c.IdConsole)).ToList()
                 , s.Id)
             ).ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            var saleModel = await _appDbContext.Sales.FirstOrDefaultAsync(s => s.Id == id);

            return new Sale(
                saleModel.CreationDate,
                _appDbContext.Concepts.Where(c => c.IdSale == id).Select(c => new Concept(c.Quantity, c.UnitPrice, c.IdConsole)).ToList(),
                saleModel.Id
                );
        }
    }
}
