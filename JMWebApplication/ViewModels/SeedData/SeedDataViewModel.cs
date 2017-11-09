using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.SeedData
{
    public class SeedDataViewModel : TransactionalInformation 
    {
        public List<Customer> Customers;
    }
}