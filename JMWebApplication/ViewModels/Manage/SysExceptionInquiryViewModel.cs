using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
  
    public class SysExceptionInquiryViewModel : TransactionalInformation
    {
        public List<SysExceptionInquiry> SysExceptionInquiry;       
    }

    public class SysExceptionInquiryDTO : DataGridPagingInformation
    {
        public string queryStr { get; set; }
    }

}
