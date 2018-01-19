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
    public class PaymentTypeController : Controller
    {
        IPaymentTypeDataService paymentTypeDataService;
       
        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public PaymentTypeController(IPaymentTypeDataService dataService)
        {
            paymentTypeDataService = dataService;           

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

            PaymentTypeApplicationService paymentTypeApplicationService = new PaymentTypeApplicationService(paymentTypeDataService);
            List<PaymentType> paymentTypes = paymentTypeApplicationService.GetPaymentTypes(out transaction);

             

            var json = new
            {
                //total = paging.TotalRows,

                rows = (from r in paymentTypes
                        select new PaymentType()
                        {
                            //Id  = r.Id,
                            //Name = r.Name,
                            PaymentTypeID = r.PaymentTypeID,
                            Description = r.Description,
                            RequiresCreditCard=r.RequiresCreditCard 
                        }).ToArray()
            };

            return Json(json, JsonRequestBehavior.AllowGet);

        }
    }
}