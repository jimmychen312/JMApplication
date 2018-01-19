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
        IManageDataService sysModuleDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public ManageController(IManageDataService dataService)
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
        public JsonResult GetTree(string Id)
        {
            if (Session["Account"] != null)
            {
                Account account = (Account)Session["Account"];

                TransactionalInformation transaction;

                SysModuleInquiryViewModel sysModuleInquiryViewModel = new SysModuleInquiryViewModel();

                ManageApplicationService sysModuleApplicationService = new ManageApplicationService(sysModuleDataService);
                List<SysModuleInquiry> sysModules = sysModuleApplicationService.GetMenuByPersonId(account.Id, Id, out transaction);

                if (Id != string.Empty)
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
            else
            {
                return Json("");
            }
            
        }

    }
}

    
