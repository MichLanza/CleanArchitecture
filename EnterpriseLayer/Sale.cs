
namespace EnterpriseLayer
{
    public class Sale
    {
        public int Id { get; }

        public DateTime Date { get; }

        public decimal Total { get; }

        public List<Concept> Concepts { get; }

        public Sale(DateTime date, List<Concept> concepts)
        {
            Date = date;
            Concepts = concepts;
            Total = GetTotal();
        }

        public Sale(DateTime date, List<Concept> concepts, int id)
        {
            Id = id;
            Date = date;
            Concepts = concepts;
            Total = GetTotal();
        }

        private decimal GetTotal() => Concepts.Sum(s => s.Price);
        
    }
}
