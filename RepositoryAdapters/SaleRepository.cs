using Application;
using DataAdapters;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using ModelAdapters;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryAdapters
{
    public class SaleRepository : IRepository<Sale>, IRepositorySearch<SaleModel, Sale>
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

        public async Task<IEnumerable<Sale>> GetAsync(Expression<Func<SaleModel, bool>> predicate)
        {
            var salesModel = await _appDbContext.Sales.Include("Concept").Where(predicate).ToListAsync();

            var sales = new List<Sale>();

            foreach (var saleModel in salesModel)
            {
                var concepts = new List<Concept>();
                foreach (var conceptModel in saleModel.Concepts)
                {
                    var concept = new Concept(conceptModel.Quantity, conceptModel.UnitPrice, conceptModel.IdConsole);
                    concepts.Add(concept);
                }

                var sale = new Sale(saleModel.CreationDate, concepts, saleModel.Id);
                sales.Add(sale);
            }

            return sales;

        }

    }
}
