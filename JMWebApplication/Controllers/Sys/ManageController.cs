using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMModels;
using JMApplication.ViewModels.Manage;
using JMApplicationService;
using JMDataServiceInterface;

namespace JMApplication.Controllers
{
    public class ManageController : Controller
    {

//        Account account = new Account();

//        account.Id = "admin";
//account.TrueName = "admin";
//Session["Account"] = account;
            
        

        
        ISysModuleDataService sysModuleDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public ManageController(ISysModuleDataService dataService)
        {
            sysModuleDataService = dataService;
        }


        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTree(string id)
        {
            TransactionalInformation transaction;

            SysModuleInquiryViewModel sysModuleInquiryViewModel = new SysModuleInquiryViewModel();

            SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);

            List<SysModuleInquiry> sysModules = sysModuleApplicationService.SysModuleInquiry(id,out transaction);
            
            if (id != string.Empty)
            {
                sysModuleInquiryViewModel.SysModules = sysModules;
                sysModuleInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
                sysModuleInquiryViewModel.ReturnMessage = transaction.ReturnMessage;

                var jsonData = (
                        from m in sysModuleInquiryViewModel.SysModules
                        select new
                        {
                            id = m.Id,
                            text = m.Name,
                            value = m.Url,
                            showcheck = false,
                            complete = false,
                            isexpand = false,
                            checkstate = 0,
                            hasChildren = m.IsLast ? false : true,
                            Icon = m.Iconic
                        }
                    ).ToArray();

                return Json(jsonData, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("");
            }
            
        }

    }
}

    
