using JMApplication.ViewModels.Manage;
using JMApplicationService;
using JMCore;
using JMDataServiceInterface;
using JMModels;
using JMWebApplication.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JMApplication.Controllers
{
    public class SysModuletestController : Controller
    {
        //IPaymentTypeDataService paymentTypeDataService;
        ISysModuletestDataService sysModuletestDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysModuletestController(ISysModuletestDataService dataService)
        {
            sysModuletestDataService = dataService;           

        }

        // GET: SysRight
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetListP()
        {
            TransactionalInformation transaction;
                        
            var account = Session["Account"] as Account;
                        
            string controller = "SysSample";

            SysModuletestApplicationService sysModuletestApplicationService = new SysModuletestApplicationService(sysModuletestDataService);
            List<SysModule> sysModulexs =sysModuletestApplicationService.GetSysModuleList(out transaction);
                         

            var json = new
            {
                //total = paging.TotalRows,

                rows = (from r in sysModulexs
                        select new SysModule()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            EnglishName = r.EnglishName,
                            ParentId = r.ParentId
                            //,
                            //Url = r.Url,
                            //Iconic = r.Iconic,
                            //Sort = r.Sort,
                            //Remark = r.Remark,
                            //Enable = r.Enable,
                            //CreatePerson = r.CreatePerson,
                            //CreateTime = r.CreateTime,
                            //IsLast = r.IsLast
                        }).ToArray()
            };

            return Json(json, JsonRequestBehavior.AllowGet);

        }
    }
}