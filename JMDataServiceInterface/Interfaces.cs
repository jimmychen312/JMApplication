using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;

namespace JMDataServiceInterface
{
    public interface IDataService
    {
        void CreateSession();
        void BeginTransaction();
        void CommitTransaction(Boolean closeSession);
        void RollbackTransaction(Boolean closeSession);
        void CloseSession();
    }

    public interface ICustomerDataService : IDataService, IDisposable
    {
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer GetCustomerByCustomerID(Guid customerID);
        List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging);
        List<PaymentType> GetPaymentTypes();
        PaymentType GetPaymentType(Guid paymentTypeID);
        void DeleteAllCustomers();

    }
}
