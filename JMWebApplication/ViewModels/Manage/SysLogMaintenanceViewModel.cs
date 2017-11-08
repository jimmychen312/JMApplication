using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
    public class SysLogMaintenanceViewModel : TransactionalInformation
    {
        public SysLog SysLog;
        //public List<PaymentType> PaymentTypes;

        public SysLogMaintenanceViewModel()
        {
            SysLog = new SysLog();
            //PaymentTypes = new List<PaymentType>();
        }
        
    }


    public class SysLogMaintenanceDTO
    {
        public string Id { get; set; }
        public string Operator { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        public string Type { get; set; }
        public string Module { get; set; }
        public DateTime? CreateTime { get; set; }
        
    }

}