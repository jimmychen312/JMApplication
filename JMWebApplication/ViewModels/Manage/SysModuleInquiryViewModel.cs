using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
  
    public class SysModuleInquiryViewModel : TransactionalInformation
    {
        public List<SysModuleInquiry> SysModules;       
    }

    //public class SysModuleDTO
    //{       
    //    public string PersonId { get; set; }
    //   // public string LastName { get; set; }
    //}

}
