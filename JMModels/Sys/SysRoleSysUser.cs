using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMModels
{
    public class SysRoleSysUser
    {
        [Key]
        public string SysUserId { get; set; }
        public string SysRoleId { get; set; }
    }
}

