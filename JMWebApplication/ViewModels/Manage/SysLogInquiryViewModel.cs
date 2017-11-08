﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
  
    public class SysLogInquiryViewModel : TransactionalInformation
    {
        public List<SysLogInquiry> SysLogInquiry;       
    }

    public class SysModuleInquiryDTO : DataGridPagingInformation
    {
        public string queryStr { get; set; }
    }

}
