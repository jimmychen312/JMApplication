using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
    public class SysSampleMaintenanceViewModel : TransactionalInformation
    {
        public SysSample SysSample;
        //public List<PaymentType> PaymentTypes;

        public SysSampleMaintenanceViewModel()
        {
            SysSample = new SysSample();
            //PaymentTypes = new List<PaymentType>();
        }
        
    }


    public class SysSampleMaintenanceDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Bir { get; set; }
        public string Photo { get; set; }
        public string Note { get; set; }
        public DateTime? CreateTime { get; set; }
        
    }

}