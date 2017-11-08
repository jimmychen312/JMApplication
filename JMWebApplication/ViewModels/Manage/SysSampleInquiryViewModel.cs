using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
  
    public class SysSampleInquiryViewModel : TransactionalInformation
    {
        public List<SysSampleInquiry> SysSampleLists;       
    }

    public class SysSampleInquiryDTO : DataGridPagingInformation
    {
        public string queryStr { get; set; }
        //public string LastName { get; set; }
    }



}
