using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JMModels
{
    public class SysUser
    {               
            [Display(Name = "ID")]
            public string Id { get; set; }
            [Display(Name = "用户名")]
            public string UserName { get; set; }
            [Display(Name = "密码")]
            public string Password { get; set; }
            [Display(Name = "姓名")]
            public string TrueName { get; set; }
            [Display(Name = "证件号码")]
            public string Card { get; set; }
            [Display(Name = "手机号码")]
            public string MobileNumber { get; set; }
            [Display(Name = "电话号码")]
            public string PhoneNumber { get; set; }
            [Display(Name = "QQ")]
            public string QQ { get; set; }
            [Display(Name = "邮箱地址")]
            public string EmailAddress { get; set; }
            [Display(Name = "其它联系方式")]
            public string OtherContact { get; set; }
            [Display(Name = "省份")]
            public string Province { get; set; }
            [Display(Name = "城市")]
            public string City { get; set; }
            [Display(Name = "街道")]
            public string Village { get; set; }
            [Display(Name = "详细地址")]
            public string Address { get; set; }
            [Display(Name = "状态")]
            public int State { get; set; }
            [Display(Name = "创建时间")]
            public string CreateTime { get; set; }
            [Display(Name = "创建人")]
            public string CreatePerson { get; set; }
            [Display(Name = "性别")]
            public string Sex { get; set; }
            [Display(Name = "出生年月")]
            public string Birthday { get; set; }
            [Display(Name = "入职日期")]
            public string JoinDate { get; set; }
            [Display(Name = "婚姻状况")]
            public string Marital { get; set; }
            [Display(Name = "政党")]
            public string Political { get; set; }
            [Display(Name = "国籍")]
            public string Nationality { get; set; }
            [Display(Name = "户籍")]
            public string Native { get; set; }
            [Display(Name = "毕业学校")]
            public string School { get; set; }
            [Display(Name = "专业")]
            public string Professional { get; set; }
            [Display(Name = "学位")]
            public string Degree { get; set; }
            [Display(Name = "部门编号")]
            public string DepId { get; set; }
            [Display(Name = "岗位编号")]
            public string PosId { get; set; }
            [Display(Name = "专长")]
            public string Expertise { get; set; }
            [Display(Name = "在职状态")]
            public string JobState { get; set; }
            [Display(Name = "相片")]
            public string Photo { get; set; }
            [Display(Name = "附件")]
            public string Attach { get; set; }
                
    }
}