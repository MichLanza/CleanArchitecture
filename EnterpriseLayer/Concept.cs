namespace EnterpriseLayer
{
    public class Concept
    {
        public int IdConsole { get; }

        public int Quantity {  get; }
        public decimal UnitPrice { get; }   

        public decimal Price { get; }
        public Concept(int quantity, decimal unitPrice, int idConsole)
        {
            IdConsole = idConsole;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Price = GetTotalPrice();
        }

        private decimal GetTotalPrice() => UnitPrice * Quantity; 

    }
}
