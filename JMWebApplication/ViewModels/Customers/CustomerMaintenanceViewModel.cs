using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JM.ViewModels.Customers
{
    public class CustomerMaintenanceViewModel : TransactionalInformation
    {
        public Customer Customer;
        public List<PaymentType> PaymentTypes;

        public CustomerMaintenanceViewModel()
        {
            Customer = new Customer();
            PaymentTypes = new List<PaymentType>();
        }
        
    }

    public class CustomerMaintenanceDTO
    {
        public Guid CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string TelePhone { get; set; }
        public string CreditCardNumber { get; set; }
        public Guid? PaymentTypeID { get; set; }
        public DateTime? CreditCardExpirationDate { get; set; }
        public string CreditCardSecurityCode { get; set; }
        public Decimal? CreditLimit { get; set; }

    }

}