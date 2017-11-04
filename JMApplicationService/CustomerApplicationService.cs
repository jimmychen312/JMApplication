using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMApplicationService
{

    public class CustomerApplicationService
    {

        ICustomerDataService _customerDataService;  

        private ICustomerDataService CustomerDataService
        {
            get { return _customerDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerApplicationService(ICustomerDataService dataService)
        {
            _customerDataService = dataService;
        }


        /// <summary>
        /// Get Customer By Customer ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Customer GetCustomerByCustomerID(Guid customerID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                CustomerDataService.CreateSession();
                Customer customer = CustomerDataService.GetCustomerByCustomerID(customerID);
                transaction.ReturnStatus = true;
                return customer;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
                return null;
            }
            finally
            {
                CustomerDataService.CloseSession();
            }

        }

        /// <summary>
        /// Customer Inquiry
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="paging"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                CustomerDataService.CreateSession();
                List<CustomerInquiry> customers = CustomerDataService.CustomerInquiry(firstName, lastName, paging);
                transaction.ReturnStatus = true;
                return customers;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
                return null;
            }
            finally
            {
                CustomerDataService.CloseSession();
            }

        }

        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<PaymentType> GetPaymentTypes(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                CustomerDataService.CreateSession();
                List<PaymentType> paymentTypes = CustomerDataService.GetPaymentTypes();
                transaction.ReturnStatus = true;
                return paymentTypes;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
                return null;
            }
            finally
            {
                CustomerDataService.CloseSession();
            }

        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void CreateCustomer(Customer customer, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();

            try
            {
                CustomerDataService.CreateSession();

                customerBusinessRules.ValidateCustomer(customer, CustomerDataService);

                if (customerBusinessRules.ValidationStatus == true)
                {
                    CustomerDataService.BeginTransaction();
                    CustomerDataService.CreateCustomer(customer);
                    CustomerDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Customer successfully created at " + customer.DateCreated.ToString());
                }
                else
                {
                    transaction.ReturnStatus = customerBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = customerBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = customerBusinessRules.ValidationErrors;
                }

            }
            catch (Exception ex)
            {
                CustomerDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                CustomerDataService.CloseSession();
            }

        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateCustomer(Customer customer, out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();
            CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();

            try
            {

                CustomerDataService.CreateSession();
                customerBusinessRules.ValidateCustomer(customer, CustomerDataService);

                if (customerBusinessRules.ValidationStatus == true)
                {
                    Customer originalCustomerInformation = CustomerDataService.GetCustomerByCustomerID(customer.CustomerID);
                    PopulateCustomerInformation(customer, originalCustomerInformation);
                    CustomerDataService.BeginTransaction();
                    CustomerDataService.UpdateCustomer(originalCustomerInformation);
                    CustomerDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("Customer successfully updated at " + DateTime.Now.ToString());
                }
                else
                {
                    transaction.ReturnStatus = customerBusinessRules.ValidationStatus;
                    transaction.ReturnMessage = customerBusinessRules.ValidationMessage;
                    transaction.ValidationErrors = customerBusinessRules.ValidationErrors;
                }

            }
            catch (Exception ex)
            {
                CustomerDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                CustomerDataService.CloseSession();
            }


        }

        public void DeleteAllCustomers(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                CustomerDataService.CreateSession();

                CustomerDataService.BeginTransaction();
                CustomerDataService.DeleteAllCustomers();
                CustomerDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("All customers deleted at " + DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                CustomerDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                CustomerDataService.CloseSession();
            }

        }


        /// <summary>
        /// Populate Customer Information
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="originalCustomerInformation"></param>
        private void PopulateCustomerInformation(Customer customer, Customer originalCustomerInformation)
        {
            originalCustomerInformation.FirstName = customer.FirstName;
            originalCustomerInformation.LastName = customer.LastName;
            originalCustomerInformation.CreditCardNumber = customer.CreditCardNumber;
            originalCustomerInformation.CreditCardSecurityCode = customer.CreditCardSecurityCode;
            originalCustomerInformation.CreditLimit = customer.CreditLimit;
            originalCustomerInformation.Address = customer.Address;
            originalCustomerInformation.City = customer.City;
            originalCustomerInformation.Country = customer.Country;
            originalCustomerInformation.PhoneNumber = customer.PhoneNumber;
//            originalCustomerInformation.TelePhone = customer.TelePhone;
            originalCustomerInformation.PostalCode = customer.PostalCode;
            originalCustomerInformation.Region = customer.Region;
            originalCustomerInformation.EmailAddress = customer.EmailAddress;
            originalCustomerInformation.PaymentTypeID = customer.PaymentTypeID;

        }

    }


}
