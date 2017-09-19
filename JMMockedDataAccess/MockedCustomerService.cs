using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using JMModels;
using JMSeedData;
using System.Linq.Dynamic;

namespace JMMockedDataAccess
{


    public class MockedCustomerService : MockedDataService, ICustomerDataService
    {

        public List<Customer> Customers { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MockedCustomerService()
        {

            TransactionalInformation transaction;

            SeedData seed = new SeedData();
            seed.LoadDataSet(out transaction);

            Customers = seed.Customers;
            PaymentTypes = seed.PaymentTypes;

        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customer"></param>
        public void CreateCustomer(Customer customer)
        {
            DateTime dateCreated = System.DateTime.Now;

            customer.CustomerID = Guid.NewGuid();
            customer.DateCreated = dateCreated;
            customer.DateUpdated = dateCreated;
            Customers.Add(customer);
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            DateTime dateUpdated = System.DateTime.Now;
            customer.DateUpdated = dateUpdated;
        }

        /// <summary>
        /// Get Customer By Customer ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer GetCustomerByCustomerID(Guid customerID)
        {
            var customerInformation = Customers.First(c => c.CustomerID == customerID);
            Customer customer = customerInformation as Customer;
            return customer;
        }

        /// <summary>
        /// Get Payment Type
        /// </summary>
        /// <param name="paymentTypeID"></param>
        /// <returns></returns>
        public PaymentType GetPaymentType(Guid paymentTypeID)
        {
            var paymentInformation = PaymentTypes.First(p => p.PaymentTypeID == paymentTypeID);
            PaymentType paymentType = paymentInformation as PaymentType;
            return paymentInformation;
        }

        /// <summary>
        /// Customer Inquiry
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging)
        {

            string sortExpression = paging.SortExpression;

            if (paging.SortDirection != string.Empty)
                sortExpression = sortExpression + " " + paging.SortDirection;

            var customerQuery = Customers.AsQueryable();

            if (firstName != null && firstName.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.FirstName.StartsWith(firstName));
            }

            if (lastName != null && lastName.Trim().Length > 0)
            {
                customerQuery = customerQuery.Where(c => c.LastName.StartsWith(lastName));
            }

            var customers = from c in customerQuery
                            join p in PaymentTypes on c.PaymentTypeID equals p.PaymentTypeID
                            select new { c.CustomerID, c.FirstName, c.LastName, c.EmailAddress, c.City, c.Country, p.Description };

            int numberOfRows = customers.Count();

            customers = customers.OrderBy(sortExpression);

            var customerList = customers.Skip((paging.CurrentPageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            paging.TotalRows = numberOfRows;
            paging.TotalPages = Utilities.CalculateTotalPages(numberOfRows, paging.PageSize);

            List<CustomerInquiry> customerInquiry = new List<CustomerInquiry>();

            foreach (var customer in customerList)
            {
                CustomerInquiry customerData = new CustomerInquiry();
                customerData.CustomerID = customer.CustomerID;
                customerData.FirstName = customer.FirstName;
                customerData.LastName = customer.LastName;
                customerData.EmailAddress = customer.EmailAddress;
                customerData.City = customer.City;
                customerData.Country = customer.Country;
                customerData.PaymentTypeDescription = customer.Description;
                customerInquiry.Add(customerData);
            }

            return customerInquiry;
        }

        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <returns></returns>
        public List<PaymentType> GetPaymentTypes()
        {

            var paymentTypesQuery = PaymentTypes.AsQueryable();
            var paymentTypes = (from p in paymentTypesQuery.OrderBy("Description") select p).ToList();

            int numberOfRows = paymentTypes.Count();

            return paymentTypes;

        }

        /// <summary>
        /// Delete All Customers
        /// </summary>
        public void DeleteAllCustomers()
        {
            Customers = new List<Customer>();
        }

    }
}
