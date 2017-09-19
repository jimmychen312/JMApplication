using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMModels
{
    public class Customer
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
        //public string TelePhone { get; set; }
        public string CreditCardNumber { get; set; }
        public Guid PaymentTypeID { get; set; }
        public DateTime? CreditCardExpirationDate { get; set; }
        public string CreditCardSecurityCode { get; set; }
        public Decimal CreditLimit { get; set; }
        public DateTime? DateApproved { get; set; }
        public int ApprovalStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class CustomerInquiry
    {
        public Guid CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PaymentTypeDescription { get; set; }
    }

    public class LoginUser
    {
        public string LoginID { get; set; }
    }
}
