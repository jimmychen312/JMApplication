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
using JMModels;
using JMSeedData;
using JMAdoDataAccess;
using JM.ViewModels.Customers;

namespace JMUnitTesting
{
    [TestFixture]
    public partial class Testing
    {

        /// <summary>
        /// Test Valid Email Address
        /// </summary>
        [Test]
        public void TestValidEmailAddress()
        {

            Customer customer = new Customer();
            customer.EmailAddress = "bgates@microsoft.com";

            ValidationRules validationRules = new ValidationRules();
            validationRules.InitializeValidationRules(customer);
            Boolean returnStatus = validationRules.ValidateEmailAddress("EmailAddress");

            Assert.AreEqual(true, returnStatus);

        }

        /// <summary>
        /// Test InValid Email Address
        /// </summary>
        [Test]
        public void TestInValidEmailAddress()
        {

            Customer customer = new Customer();
            customer.EmailAddress = "bgates@microsoft";

            ValidationRules validationRules = new ValidationRules();
            validationRules.InitializeValidationRules(customer);
            Boolean returnStatus = validationRules.ValidateEmailAddress("EmailAddress");

            Assert.AreEqual(false, returnStatus);

        }

        /// <summary>
        /// Validate Cusomer With Credit Card Payment
        /// </summary>
        [Test]
        public void ValidateCustomerWithCreditCardPayment()
        {

            TransactionalInformation transaction;

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);
            List<PaymentType> paymentTypes = customerApplicationService.GetPaymentTypes(out transaction);

            var paymentType = (from p in paymentTypes where p.Description == "Visa" select p).First();

            Customer customer = new Customer();
            customer.FirstName = "Bill";
            customer.LastName = "Gates";
            customer.EmailAddress = "BGates@Microsoft.com";
            customer.PhoneNumber = "15976111157";
            customer.PaymentTypeID = paymentType.PaymentTypeID;
            customer.CreditCardNumber = "1112223333";
            customer.CreditCardExpirationDate = Convert.ToDateTime("12/31/2014");
            customer.CreditCardSecurityCode = "111";

            customerDataService.CreateSession();
            CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();
            customerBusinessRules.ValidateCustomer(customer, customerDataService);
            customerDataService.CloseSession();

            string returnMessage = Utilities.GetReturnMessage(customerBusinessRules.ValidationMessage);

            Assert.AreEqual(true, customerBusinessRules.ValidationStatus, returnMessage);

        }

        /// <summary>
        /// Validate Customer With Check Payment
        /// </summary>
        [Test]
        public void ValidateCustomerWithCheckPayment()
        {

            TransactionalInformation transaction;

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);
            List<PaymentType> paymentTypes = customerApplicationService.GetPaymentTypes(out transaction);

            var paymentType = (from p in paymentTypes where p.Description == "Check" select p).First();

            Customer customer = new Customer();
            customer.FirstName = "Bill";
            customer.LastName = "Gates";
            customer.EmailAddress = "BGates@Microsoft.com";
            customer.PhoneNumber = "15976111157";
            customer.PaymentTypeID = paymentType.PaymentTypeID;
            customer.CreditLimit = 1000.00m;

            customerDataService.CreateSession();
            CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();
            customerBusinessRules.ValidateCustomer(customer, customerDataService);
            customerDataService.CloseSession();

            string returnMessage = Utilities.GetReturnMessage(customerBusinessRules.ValidationMessage);

            Assert.AreEqual(true, customerBusinessRules.ValidationStatus, returnMessage);

        }

    }
}
