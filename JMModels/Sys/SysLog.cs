using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMModels
{
   public class SysLog
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "操作人")]
        public string Operator { get; set; }

        [Display(Name = "信息")]
        public string Message { get; set; }

        [Display(Name = "结果")]
        public string Result { get; set; }

        [Display(Name = "类型")]
        public string Type { get; set; }

        [Display(Name = "模块")]
        public string Module { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }

    public class SysLogList
    {
        public string Id { get; set; }
        public string Operator { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        public string Type { get; set; }
        public string Module { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}

