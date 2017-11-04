using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMModels
{

    public class SysModule
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "英文名称")]
        public string EnglishName { get; set; }

        [Display(Name = "父节点")]
        public string ParentId { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }

        [Display(Name = "图标")]
        public string Iconic { get; set; }

        [Display(Name = "排序")]
        public int? Sort { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "状态")]
        public bool? State { get; set; }

        [Display(Name = "创建人")]
        public string CreatePerson { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "是否最后")]
        public bool IsLast { get; set; }

        [Display(Name = "版本")]
        public string version { get; set; }

    }

    public class SysModuleInquiry
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string Iconic { get; set; }
        public int? Sort { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public string CreatePerson { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool IsLast { get; set; }
        public string version { get; set; }
    }
}
