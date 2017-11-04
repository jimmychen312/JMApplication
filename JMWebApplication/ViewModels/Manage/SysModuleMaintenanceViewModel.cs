using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JM.ViewModels.Manage
{
    public class SysModuleMaintenanceViewModel : TransactionalInformation
    {

        //public Customer Customer;
        public SysModule SysModule;

        //public List<PaymentType> PaymentTypes;

        public SysModuleMaintenanceViewModel()
        {
             SysModule = new SysModule();
            //PaymentTypes = new List<PaymentType>();
        }
        
    }

    public class SysModuleMaintenanceDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string Iconic { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public Boolean State { get; set; }
        public string CreatePerson { get; set; }
        public DateTime CreateTime { get; set; }
        public Boolean IsLast { get; set; }
        
    }

}