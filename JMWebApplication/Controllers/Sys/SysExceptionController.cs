using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using JMApplication.ViewModels.Manage;
using JMApplicationService;
using JMDataServiceInterface;
using JMModels;
using JMApplication.Helpers;
using JMWebApplication.Controllers;

namespace JMApplication.Controllers
{
    public class SysExceptionController : BaseController
    {
        ISysExceptionDataService sysExceptionDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysExceptionController(ISysExceptionDataService dataService)
        {
            sysExceptionDataService = dataService;
        }


        public ActionResult Error()
        {

            BaseException ex = new BaseException();
            return View(ex);
        }


        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>json</returns>
        /// 

        public JsonResult SysExceptionInquiry(string queryStr, int page, int rows, string SortExpression, string SortDirection)
        {
            TransactionalInformation transaction;

            if (queryStr == null) queryStr = string.Empty;
            if (SortDirection == null) SortDirection = string.Empty;
            if (SortExpression == null) SortExpression = string.Empty;

            SysExceptionInquiryViewModel sysExceptionInquiryViewModel = new SysExceptionInquiryViewModel();

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = page;
            paging.PageSize = rows;
            paging.SortExpression = SortExpression; ;
            paging.SortDirection = SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "ASC";
            if (paging.SortExpression == "") paging.SortExpression = "Id";

            SysExceptionApplicationService sysExceptionApplicationService = new SysExceptionApplicationService(sysExceptionDataService);
            List<SysExceptionInquiry> sysExceptions = sysExceptionApplicationService.SysExceptionInquiry(queryStr,paging, out transaction);

            //if (id != string.Empty)
            //{
                sysExceptionInquiryViewModel.SysExceptionInquiry = sysExceptions;
                sysExceptionInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
                sysExceptionInquiryViewModel.ReturnMessage = transaction.ReturnMessage;

                var json = new
                {
                    total = paging.TotalRows,
                    rows = (from m in sysExceptionInquiryViewModel.SysExceptionInquiry
                            select new SysException()
                            {
                                Id = m.Id,
                                HelpLink = m.HelpLink,
                                Message = m.Message,
                                Source = m.Source,
                                StackTrace = m.StackTrace,
                                TargetSite = m.TargetSite,
                                Data = m.Data,
                                CreateTime = m.CreateTime
                            }
                            ).ToArray()
                };

           
                return Json(json);
            //}
            //else
            //{
            //    return Json("");
            //}
                       
        }


        public ActionResult Index()
        {

            return View();

        }

        #region 详细

        public ActionResult Details(string Id, SysExceptionMaintenanceDTO sysExceptionDTO)
        {
            TransactionalInformation transaction;

            SysExceptionMaintenanceViewModel sysExceptionMaintenanceViewModel = new SysExceptionMaintenanceViewModel();

            SysException sysException = new SysException();

            ModelStateHelper.UpdateViewModel(sysExceptionDTO, sysException);

            SysExceptionApplicationService sysExceptionApplicationService = new SysExceptionApplicationService(sysExceptionDataService);
            sysException = sysExceptionApplicationService.GetSysExceptionById(Id, out transaction);

            return View(sysException);
        }
        #endregion


        #region 删除
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// Delete SysException
        /// </summary>
        /// <param name="postedFormData"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Delete(FormCollection postedFormData, [System.Web.Http.FromBody] SysExceptionMaintenanceDTO sysExceptionDTO)
        {
            TransactionalInformation transaction;

            SysExceptionMaintenanceViewModel sysExceptionMaintenanceViewModel = new SysExceptionMaintenanceViewModel();

            SysException sysException = new SysException();

            ModelStateHelper.UpdateViewModel(sysExceptionDTO, sysException);

            SysExceptionApplicationService sysExceptionApplicationService = new SysExceptionApplicationService(sysExceptionDataService);
            sysExceptionApplicationService.DeleteSysExceptionById(sysException.Id, out transaction);

            sysExceptionMaintenanceViewModel.SysException = sysException;
            sysExceptionMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            sysExceptionMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            sysExceptionMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;


            if (transaction.ReturnStatus == false)
            {
                //var Json = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                //return badresponse;

                //return Json(new
                //{
                //    ReturnStatus = sysExceptionMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysExceptionMaintenanceViewModel,
                //    ValidationErrors = sysExceptionMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysExceptionMaintenanceViewModel),
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
                //    ReturnStatus = sysExceptionMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysExceptionMaintenanceViewModel,
                //    ValidationErrors = sysExceptionMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysExceptionMaintenanceViewModel),
                //    JsonRequestBehavior.AllowGet
                //});

                return Json(1, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}