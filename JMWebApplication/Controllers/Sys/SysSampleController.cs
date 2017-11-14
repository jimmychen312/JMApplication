using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMModels;
using JMApplication.ViewModels.Manage;
using JMApplicationService;
using JMDataServiceInterface;
using JMApplication.Helpers;
using System.Net;
using System.Net.Http;
using JMApplication.ViewModels;
using JMApplication.ViewModels.Customers;
using JMEFDataAccess;
using JMApplication.Filters;
using System.Web.Security;
using JMCore;
using JMCommon;
using JMWebApplication.Controllers;

namespace JMApplication.Controllers
{
    public class SysSampleController : BaseController
    {        
        ISysSampleDataService sysSampleDataService;
        ISysLogDataService sysLogDataService;
        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysSampleController(ISysSampleDataService dataService,ISysLogDataService dataService2)
        {
            sysSampleDataService = dataService;
            sysLogDataService = dataService2;
        }

        [SupportFilter]
        public ActionResult Test()
        {
            return View();
        }

        // GET: SysSample
        public ActionResult Index()
        {
            return View();
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
            paging.SortExpression = SortExpression;;
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


        #region 创建
        
        public ActionResult Create()
        {
            return View();
        }
                 

        /// <summary>
        /// Create SysSample
        /// </summary>
        /// <param name="postedFormData"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Create (FormCollection postedFormData,[System.Web.Http.FromBody] SysSampleMaintenanceDTO sysSampleDTO)
        {
            TransactionalInformation transaction;
                        
            SysSampleMaintenanceViewModel sysSampleMaintenanceViewModel = new SysSampleMaintenanceViewModel();

            SysSample sysSample = new SysSample();
                        
            ModelStateHelper.UpdateViewModel(sysSampleDTO, sysSample);
            
            SysSampleApplicationService sysSampleApplicationService = new SysSampleApplicationService(sysSampleDataService);
            sysSampleApplicationService.CreateSysSample(sysSample, out transaction);

            sysSampleMaintenanceViewModel.SysSample = sysSample;
            sysSampleMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            sysSampleMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            sysSampleMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;


            //LogHandler logHander = new LogHandler(sysLogDataService);
            
            if (transaction.ReturnStatus == false)
            {
                //var Json = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                //return badresponse;

                //return Json(new
                //{
                //    ReturnStatus = sysSampleMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysSampleMaintenanceViewModel,
                //    ValidationErrors = sysSampleMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysSampleMaintenanceViewModel),
                //    //JsonRequestBehavior.AllowGet
                //});


                LogHandler.WriteServiceLog("虚拟用户", "Id:" + sysSample.Id + ",Name:" + sysSample.Name, "失败", "创建", "样例程序");
                return Json(JsonHandler.CreateMessage(0, "插入失败" + sysSampleMaintenanceViewModel.ReturnMessage), JsonRequestBehavior.AllowGet);
                //return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var response = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.Created, customerMaintenanceViewModel);
                //return response;

                //return Json(new
                //{
                //    ReturnStatus = sysSampleMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysSampleMaintenanceViewModel,
                //    ValidationErrors = sysSampleMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysSampleMaintenanceViewModel),
                //    JsonRequestBehavior.AllowGet
                //});

                LogHandler.WriteServiceLog("虚拟用户", "Id:" + sysSample.Id + ",Name:" + sysSample.Name, "成功", "创建", "样例程序");
                return Json(JsonHandler.CreateMessage(1, "插入成功"), JsonRequestBehavior.AllowGet);
                //return Json(1, JsonRequestBehavior.AllowGet);
            }
            
        }

        #endregion


        #region 更改
               
        public ActionResult Edit(string Id,SysSampleMaintenanceDTO sysSampleDTO)
        {
            TransactionalInformation transaction;

            SysSampleMaintenanceViewModel sysSampleMaintenanceViewModel = new SysSampleMaintenanceViewModel();
            
            SysSample sysSample = new SysSample();

            ModelStateHelper.UpdateViewModel(sysSampleDTO, sysSample);

            SysSampleApplicationService sysSampleApplicationService = new SysSampleApplicationService(sysSampleDataService);
            sysSample = sysSampleApplicationService.GetSysSampleById(Id, out transaction);

            return View(sysSample);
        }

        /// <summary>
        /// Update SysSample
        /// </summary>
        /// <param name="postedFormData"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Edit(FormCollection postedFormData, [System.Web.Http.FromBody] SysSampleMaintenanceDTO sysSampleDTO)
        {
            TransactionalInformation transaction;

            SysSampleMaintenanceViewModel sysSampleMaintenanceViewModel = new SysSampleMaintenanceViewModel();

            SysSample sysSample = new SysSample();

            ModelStateHelper.UpdateViewModel(sysSampleDTO, sysSample);

            SysSampleApplicationService sysSampleApplicationService = new SysSampleApplicationService(sysSampleDataService);
            sysSampleApplicationService.UpdateSysSample(sysSample, out transaction);

            sysSampleMaintenanceViewModel.SysSample = sysSample;
            sysSampleMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            sysSampleMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            sysSampleMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;


            if (transaction.ReturnStatus == false)
            {
                //var Json = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                //return badresponse;

                //return Json(new
                //{
                //    ReturnStatus = sysSampleMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysSampleMaintenanceViewModel,
                //    ValidationErrors = sysSampleMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysSampleMaintenanceViewModel),
                //    //JsonRequestBehavior.AllowGet
                //});

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var response = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.Created, customerMaintenanceViewModel);
                //return response;

                //return Json(new
                //{
                //    ReturnStatus = sysSampleMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysSampleMaintenanceViewModel,
                //    ValidationErrors = sysSampleMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysSampleMaintenanceViewModel),
                //    JsonRequestBehavior.AllowGet
                //});

                return Json(1, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion


        #region 详细

        public ActionResult Details(string Id, SysSampleMaintenanceDTO sysSampleDTO)
        {
            TransactionalInformation transaction;

            SysSampleMaintenanceViewModel sysSampleMaintenanceViewModel = new SysSampleMaintenanceViewModel();

            SysSample sysSample = new SysSample();

            ModelStateHelper.UpdateViewModel(sysSampleDTO, sysSample);

            SysSampleApplicationService sysSampleApplicationService = new SysSampleApplicationService(sysSampleDataService);
            sysSample = sysSampleApplicationService.GetSysSampleById(Id, out transaction);

            return View(sysSample);
        }
        #endregion


        #region 删除
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// Delete SysSample
        /// </summary>
        /// <param name="postedFormData"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Delete(FormCollection postedFormData, [System.Web.Http.FromBody] SysSampleMaintenanceDTO sysSampleDTO)
        {
            TransactionalInformation transaction;

            SysSampleMaintenanceViewModel sysSampleMaintenanceViewModel = new SysSampleMaintenanceViewModel();

            SysSample sysSample = new SysSample();

            ModelStateHelper.UpdateViewModel(sysSampleDTO, sysSample);

            SysSampleApplicationService sysSampleApplicationService = new SysSampleApplicationService(sysSampleDataService);
            sysSampleApplicationService.DeleteSysSampleById(sysSample.Id, out transaction);

            sysSampleMaintenanceViewModel.SysSample = sysSample;
            sysSampleMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            sysSampleMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            sysSampleMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;


            if (transaction.ReturnStatus == false)
            {
                //var Json = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                //return badresponse;

                //return Json(new
                //{
                //    ReturnStatus = sysSampleMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysSampleMaintenanceViewModel,
                //    ValidationErrors = sysSampleMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysSampleMaintenanceViewModel),
                //    //JsonRequestBehavior.AllowGet
                //});

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var response = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.Created, customerMaintenanceViewModel);
                //return response;

                //return Json(new
                //{
                //    ReturnStatus = sysSampleMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysSampleMaintenanceViewModel,
                //    ValidationErrors = sysSampleMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysSampleMaintenanceViewModel),
                //    JsonRequestBehavior.AllowGet
                //});

                return Json(1, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}