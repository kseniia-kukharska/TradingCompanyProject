using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompanyDto;

namespace TradingCompanyDal.Interfaces
{
    public interface ICustomerDal
    {
        Customer Create(Customer customer);
        List<Customer> GetAll();
        Customer GetByID(int customerID);
        Customer Update(Customer customer);
        bool Delete(int customerID);
    }
}
