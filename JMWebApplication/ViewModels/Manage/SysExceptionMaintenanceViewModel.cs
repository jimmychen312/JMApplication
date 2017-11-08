using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
    public class SysExceptionMaintenanceViewModel : TransactionalInformation
    {
        public SysException SysException;
        //public List<PaymentType> PaymentTypes;

        public SysExceptionMaintenanceViewModel()
        {
            SysException = new SysException();
            //PaymentTypes = new List<PaymentType>();
        }
        
    }


    public class SysExceptionMaintenanceDTO
    {
        public string Id { get; set; }
        public string HelpLink { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
        public string Data { get; set; }
        public DateTime? CreateTime { get; set; }

    }

}