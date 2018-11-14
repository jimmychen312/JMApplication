using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
    public class AccountViewModel : TransactionalInformation
    {
        public SysUser SysUser;
        
        public AccountViewModel()
        {
            SysUser = new SysUser();
        }        
    }
    
    public class SysUserMaintenanceDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Boolean State { get; set; }
    }
     
}