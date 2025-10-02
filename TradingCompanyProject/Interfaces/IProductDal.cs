using TradingCompanyDto;

namespace TradingCompanyDal.Interfaces
{
    public interface IProductDal
    {
        Product Create(Product product);
        List<Product> GetAll();
        Product GetByID(int productID);
        Product Update(Product product);
        bool Delete(int productID);
    }
     
}