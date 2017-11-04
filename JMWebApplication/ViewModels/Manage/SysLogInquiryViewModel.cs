using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JM.ViewModels.Manage
{
  
    public class SysLogInquiryViewModel : TransactionalInformation
    {
        public List<SysLogList> SysLogLists;       
    }

    //public class SysModuleInquiryDTO : DataGridPagingInformation
    //{       
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }      
    //}

}
