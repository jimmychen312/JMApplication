using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMModels
{     
    /// <summary>
    /// 异常处理类
    /// </summary>
        public class SysException
        {
            [Display(Name = "ID")]
            public string Id { get; set; }

            [Display(Name = "帮助链接")]
            public string HelpLink { get; set; }

            [Display(Name = "错误信息")]
            public string Message { get; set; }

            [Display(Name = "来源")]
            public string Source { get; set; }

            [Display(Name = "堆栈")]
            public string StackTrace { get; set; }

            [Display(Name = "目标页")]
            public string TargetSite { get; set; }

            [Display(Name = "程序集")]
            public string Data { get; set; }

            [Display(Name = "发生时间")]
            public DateTime? CreateTime { get; set; }
        }

    public class SysExceptionInquiry
    {
        public string Id { get; set; }
        public string HelpLink { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
        public string Data { get; set; }
        public DateTime? CreateTime { get; set; }
    }

}
