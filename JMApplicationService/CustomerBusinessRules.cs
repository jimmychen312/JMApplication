using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using JMModels;

namespace JMApplicationService
{

    public class CustomerBusinessRules : ValidationRules
    {

        ICustomerDataService customerDataService;

        /// <summary>
        /// Initialize Customer Business Rules
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="dataService"></param>
        public void InitializeCustomerBusinessRules(Customer customer, ICustomerDataService dataService)
        {
            customerDataService = dataService;
            InitializeValidationRules(customer);
        }

        /// <summary>
        /// Validate Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="dataService"></param>
        public void ValidateCustomer(Customer customer, ICustomerDataService dataService)
        {
            customerDataService = dataService;

            InitializeValidationRules(customer);

            ValidateRequired("FirstName", "First Name");
            ValidateRequired("LastName", "Last Name");
            ValidateRequired("EmailAddress", "Email Address");
            ValidateEmailAddress("EmailAddress", "Email Address");
            ValidateRequired("PhoneNumber", "Phone Number");
            //ValidatePhoneNumber("PhoneNumber", "Phone Number");
            ValidateCreditInformation(customer);

        }

        /// <summary>
        /// Validate Credit Information
        /// </summary>
        /// <param name="customer"></param>
        public void ValidateCreditInformation(Customer customer)
        {

            if (ValidateGuidRequired("PaymentTypeID", "Payment Type", "PaymentTypeID") == false) return;

            PaymentType paymentType = customerDataService.GetPaymentType(customer.PaymentTypeID);

            if (paymentType.RequiresCreditCard == (int)RequiresCreditCard.Yes)
            {
                ValidateRequired("CreditCardNumber", "For selected payment type, Credit Card Number");
                ValidateNumeric("CreditCardNumber", "For selected payment type, Credit Card Number");
                ValidateRequired("CreditCardSecurityCode", "For selected payment type, Credit Card Security Code");
                ValidateNumeric("CreditCardSecurityCode", "For selected payment type, Credit Card Security Code");
                ValidateRequiredDate("CreditCardExpirationDate", "For selected payment type, Credit Card Expiration Date");

            }
            else
            {
                ValidateDecimalIsNotZero("CreditLimit", "For selected payment type, Credit Limit");
            }

        }

        public enum RequiresCreditCard
        {
            Yes = 1,
            No = 0
        }


    }

}
