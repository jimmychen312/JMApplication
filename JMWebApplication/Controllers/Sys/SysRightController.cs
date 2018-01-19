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
    public class SysRightController : BaseController
    {
        ISysSampleDataService sysSampleDataService;
        ISysRightDataService sysRightDataService;
        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysRightController(ISysRightDataService dataService, ISysSampleDataService dataService2)
        {
            sysRightDataService = dataService;
            sysSampleDataService = dataService2;
            
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

            //SysRightInquiryViewModel sysRightInquiryViewModel = new SysRightInquiryViewModel();

            var account = Session["Account"] as Account;                        

            //string accountId = "admin";
            string controller = "SysSample";

            SysRightApplicationService sysRightApplicationService = new SysRightApplicationService(sysRightDataService);
            List<Permission> permissions = sysRightApplicationService.GetPermissions(account.Id, controller, out transaction);

            //SysRightApplicationService sysSampleApplicationService = new SysRightApplicationService(sysSampleDataService);
            //List<SysRightInquiry> sysSamples = sysSampleApplicationService.GetSysRightInquiry(queryStr, paging, out transaction);

            //if (id != string.Empty)
            //{
            //sysRightInquiryViewModel.PermissionLists = permissions;
            //sysRightInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
            //sysRightInquiryViewModel.ReturnMessage = transaction.ReturnMessage;

            //sysSampleInquiryViewModel.TotalPages = paging.TotalPages;
            //sysSampleInquiryViewModel.TotalRows = paging.TotalRows;
            //sysSampleInquiryViewModel.PageSize = paging.PageSize;
            //var json = permissionInquiryViewModel.TotalRows;

            var json = new
            {
                //total = paging.TotalRows,

                rows = (from r in permissions
                        select new Permission()
                        {
                            //Id  = r.Id,
                            //Name = r.Name,
                            KeyCode=r.KeyCode,
                            IsValid =r.IsValid

                        }).ToArray()
            };

            return Json(json, JsonRequestBehavior.AllowGet);

        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetList(string queryStr, int page, int rows, string SortExpression, string SortDirection)
        {
            //int total = 0;

            TransactionalInformation transaction;

            if (queryStr == null) queryStr = string.Empty;
            if (SortDirection == null) SortDirection = string.Empty;
            if (SortExpression == null) SortExpression = string.Empty;

            SysSampleInquiryViewModel sysSampleInquiryViewModel = new SysSampleInquiryViewModel();

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = page;
            paging.PageSize = rows;
            paging.SortExpression = SortExpression; ;
            paging.SortDirection = SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "ASC";
            if (paging.SortExpression == "") paging.SortExpression = "Id";

            SysSampleApplicationService sysSampleApplicationService = new SysSampleApplicationService(sysSampleDataService);
            List<SysSampleInquiry> sysSamples = sysSampleApplicationService.GetSysSampleInquiry(queryStr, paging, out transaction);

            //if (id != string.Empty)
            //{
            sysSampleInquiryViewModel.SysSampleLists = sysSamples;
            sysSampleInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
            sysSampleInquiryViewModel.ReturnMessage = transaction.ReturnMessage;

            //sysSampleInquiryViewModel.TotalPages = paging.TotalPages;
            //sysSampleInquiryViewModel.TotalRows = paging.TotalRows;
            //sysSampleInquiryViewModel.PageSize = paging.PageSize;

            var json = new
            {
                total = paging.TotalRows,

                rows = (from r in sysSampleInquiryViewModel.SysSampleLists
                        select new SysSample()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Age = r.Age,
                            Bir = r.Bir,
                            Photo = r.Photo,
                            Note = r.Note,
                            CreateTime = r.CreateTime,

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);

        }

    }
}