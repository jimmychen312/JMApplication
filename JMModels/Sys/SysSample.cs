using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMModels
{
    public class SysSample
    {       
            [Display(Name = "ID")]
            public string Id { get; set; }

            [Display(Name = "名称")]
            public string Name { get; set; }

            [Display(Name = "年龄")]
            public string Age { get; set; }

            [Display(Name = "生日")]
            public string Bir { get; set; }

            [Display(Name = "相片")]
            public string Photo { get; set; }

            [Display(Name = "注解")]
            public string Note { get; set; }
                    
            [Display(Name = "创建时间")]
            public DateTime? CreateTime { get; set; }
        
    }

    public class SysSampleInquiry
    {        
        public string Id { get; set; }              
        public string Name { get; set; }       
        public string Age { get; set; }       
        public string Bir { get; set; }       
        public string Photo { get; set; }        
        public string Note { get; set; }      
        public DateTime? CreateTime { get; set; }
    }
}
