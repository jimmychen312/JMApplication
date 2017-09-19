using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using JMWebApplication.Controllers;
using JMApplicationService;
using JMDataServiceInterface;
using JMEFDataAccess;
using JMMockedDataAccess;
using JMModels;
using JMSeedData;
using JMAdoDataAccess;
using JM.ViewModels.Customers;

namespace JMUnitTesting
{
    [TestFixture]
    public partial class Testing
    {
        ICustomerDataService customerDataService;

        /// <summary>
        /// Initialize Dependencies
        /// </summary>
        [SetUp]
        public void InitializeDependencies()
        {
            string integrationType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["IntegrationType"]);

            if (integrationType == "EntityFramework")
                customerDataService = new EFCustomerService();
            else if (integrationType == "Ado")
                customerDataService = new AdoCustomerService();
            else
                customerDataService = new MockedCustomerService();

        }

        /// <summary>
        /// Create Customer Integration Test
        /// </summary>
        [Test]
        public void CreateCustomerIntegrationTest()
        {

            string returnMessage;

            TransactionalInformation transaction;

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);
            List<PaymentType> paymentTypes = customerApplicationService.GetPaymentTypes(out transaction);

            var paymentType = (from p in paymentTypes where p.Description == "Visa" select p).First();

            Customer customer = new Customer();
            customer.FirstName = "William";
            customer.LastName = "Gates";
            customer.EmailAddress = "bgates@microsoft.com";
            customer.PhoneNumber = "08615976111157";
            customer.Address = "One Microsoft Way";
            customer.City = "Redmond";
            customer.Region = "WA";
            customer.PostalCode = "98052-7329";
            customer.PaymentTypeID = paymentType.PaymentTypeID;
            customer.CreditCardExpirationDate = Convert.ToDateTime("12/31/2014");
            customer.CreditCardSecurityCode = "111";
            customer.CreditCardNumber = "123111234";

            customerApplicationService.CreateCustomer(customer, out transaction);

            returnMessage = Utilities.GetReturnMessage(transaction.ReturnMessage);

            Assert.AreEqual(true, transaction.ReturnStatus, returnMessage);

        }

        /// <summary>
        /// Update Customer Integration Test
        /// </summary>
        [Test]
        public void UpdateCustomerIntegrationTest()
        {

            string returnMessage;

            TransactionalInformation transaction;

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = 1;
            paging.PageSize = 15;
            paging.SortExpression = "LastName";
            paging.SortDirection = "ASC";

            List<CustomerInquiry> customers = customerApplicationService.CustomerInquiry("", "", paging, out transaction);

            var customerInformation = (from c in customers select c).First();
            Guid customerID = customerInformation.CustomerID;

            Customer customer = customerApplicationService.GetCustomerByCustomerID(customerID, out transaction);

            customerApplicationService.UpdateCustomer(customer, out transaction);
            returnMessage = Utilities.GetReturnMessage(transaction.ReturnMessage);

            Assert.AreEqual(true, transaction.ReturnStatus, returnMessage);

        }

        /// <summary>
        /// Customer Inquiry Integration Test
        /// </summary>
        [Test]
        public void CustomerInquiryIntegrationTest()
        {

            TransactionalInformation transaction;

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = 1;
            paging.PageSize = 15;
            paging.SortExpression = "LastName";
            paging.SortDirection = "ASC";

            List<CustomerInquiry> customers = customerApplicationService.CustomerInquiry("", "", paging, out transaction);

            string returnMessage = Utilities.GetReturnMessage(transaction.ReturnMessage);

            Assert.AreEqual(15, customers.Count, returnMessage);

        }



    }
}
