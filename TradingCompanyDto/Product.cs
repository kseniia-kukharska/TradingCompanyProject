
namespace TradingCompanyDto
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; } // Можливо, варто змінити назву на QuantityInStock, але залишаю Amount, як на діаграмі
    }
}