using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMModels;
using JM.ViewModels.Manage;
using JMApplicationService;
using JMDataServiceInterface;
using JM.Helpers;
using System.Net;
using System.Net.Http;
using JM.ViewModels;
using JM.ViewModels.Customers;
using JMEFDataAccess;
using JM.Filters;
using System.Web.Security;

namespace JMApplication.Controllers
{
    public class SysSampleController : Controller
    {        
        ISysSampleDataService sysSampleDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysSampleController(ISysSampleDataService dataService)
        {
            sysSampleDataService = dataService;
        }


        // GET: SysSample
        public ActionResult Index()
        {
            return View();
        }

        
        [System.Web.Mvc.HttpPost]
        public JsonResult GetList(int page, int rows, string SortExpression, string SortDirection)
        {
            //int total = 0;

            TransactionalInformation transaction;
                        
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
            List<SysSampleInquiry> sysSamples = sysSampleApplicationService.GetSysSampleInquiry(paging, out transaction);

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

    }
}