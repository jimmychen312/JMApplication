using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMModels;

namespace JMApplication.ViewModels.Manage
{
    public class SysModuleOperateMaintenancViewModel : TransactionalInformation
    {       
        public SysModuleOperate SysModuleOperate;
                
        public SysModuleOperateMaintenancViewModel()
        {
            SysModuleOperate = new SysModuleOperate();
        }        
    }

    public class SysModuleOperateMaintenanceDTO
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string KeyCode { get; set; }
        public string ModuleId { get; set; }
        public bool IsValid { get; set; }
        public int Sort { get; set; }
    }

}