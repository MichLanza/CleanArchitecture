using Application;
using EnterpriseLayer;
using MapperAdapter.Dto.Request;

namespace MapperAdapter
{
    public class SaleMapper : IMapper<SaleRequestDto, Sale>
    {
        public Sale Map(SaleRequestDto dto)
        {
            var concepts = new List<Concept>();

            foreach (var concpetDto in dto.Concepts)
            {
                concepts.Add(new Concept(concpetDto.Quantity, concpetDto.UnitPrice, concpetDto.IdConsole));
            }

            return new Sale(DateTime.Now, concepts);
        }
    }
}
