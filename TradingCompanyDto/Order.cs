
namespace TradingCompanyDto
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int StatusID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
