using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
  
    public class SysRightInquiryViewModel : TransactionalInformation
    {
        public List<Permission> PermissionLists;       
    }

    public class SysRightInquiryDTO : DataGridPagingInformation
    {
        public string queryStr { get; set; }
        //public string LastName { get; set; }
    }



}
