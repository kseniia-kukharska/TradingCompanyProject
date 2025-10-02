using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompanyDto;

namespace TradingCompanyDal.Interfaces
{
    public interface IProductDal
    {
        Product Create(Product product);
        List<Product> GetAll();
        Product GetById(int productId);
        Product Update(Product product);
        bool Delete(int productId);
    }
     
}