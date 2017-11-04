using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JM.ViewModels.Manage
{
  
    public class SysModuleInquiryViewModel : TransactionalInformation
    {
        public List<SysModuleInquiry> SysModules;       
    }

    //public class SysModuleInquiryDTO : DataGridPagingInformation
    //{       
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }      
    //}

}
