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
    public class SysLogController : BaseController
    {
        ISysLogDataService sysLogDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysLogController(ISysLogDataService dataService)
        {
            sysLogDataService = dataService;
        }



        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>json</returns>
        
        public JsonResult SysLogInquiry(string queryStr, int page, int rows, string SortExpression, string SortDirection)
        {
            TransactionalInformation transaction;

            if (queryStr == null) queryStr = string.Empty;
            if (SortDirection == null) SortDirection = string.Empty;
            if (SortExpression == null) SortExpression = string.Empty;

            SysLogInquiryViewModel sysLogInquiryViewModel = new SysLogInquiryViewModel();

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = page;
            paging.PageSize = rows;
            paging.SortExpression = SortExpression; ;
            paging.SortDirection = SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "ASC";
            if (paging.SortExpression == "") paging.SortExpression = "Id";

            SysLogApplicationService sysLogApplicationService = new SysLogApplicationService(sysLogDataService);
            List<SysLogInquiry> sysLogs = sysLogApplicationService.SysLogInquiry(queryStr,paging, out transaction);

            //if (id != string.Empty)
            //{
                sysLogInquiryViewModel.SysLogInquiry = sysLogs;
                sysLogInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
                sysLogInquiryViewModel.ReturnMessage = transaction.ReturnMessage;

                var json = new
                {
                    total = paging.TotalRows,
                    rows = (from m in sysLogInquiryViewModel.SysLogInquiry
                            select new SysLog()
                            {
                                Id = m.Id,
                                Operator = m.Operator,
                                Message = m.Message,
                                Result = m.Result,
                                Type = m.Type,
                                Module = m.Module,
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

        public ActionResult Details(string Id, SysLogMaintenanceDTO sysLogDTO)
        {
            TransactionalInformation transaction;

            SysLogMaintenanceViewModel sysLogMaintenanceViewModel = new SysLogMaintenanceViewModel();

            SysLog sysLog = new SysLog();

            ModelStateHelper.UpdateViewModel(sysLogDTO, sysLog);

            SysLogApplicationService sysLogApplicationService = new SysLogApplicationService(sysLogDataService);
            sysLog = sysLogApplicationService.GetSysLogById(Id, out transaction);

            return View(sysLog);
        }
        #endregion


        #region 删除
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// Delete SysLog
        /// </summary>
        /// <param name="postedFormData"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Delete(FormCollection postedFormData, [System.Web.Http.FromBody] SysLogMaintenanceDTO sysLogDTO)
        {
            TransactionalInformation transaction;

            SysLogMaintenanceViewModel sysLogMaintenanceViewModel = new SysLogMaintenanceViewModel();

            SysLog sysLog = new SysLog();

            ModelStateHelper.UpdateViewModel(sysLogDTO, sysLog);

            SysLogApplicationService sysLogApplicationService = new SysLogApplicationService(sysLogDataService);
            sysLogApplicationService.DeleteSysLogById(sysLog.Id, out transaction);

            sysLogMaintenanceViewModel.SysLog = sysLog;
            sysLogMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            sysLogMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            sysLogMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;


            if (transaction.ReturnStatus == false)
            {
                //var Json = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                //return badresponse;

                //return Json(new
                //{
                //    ReturnStatus = sysLogMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysLogMaintenanceViewModel,
                //    ValidationErrors = sysLogMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysLogMaintenanceViewModel),
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
                //    ReturnStatus = sysLogMaintenanceViewModel.ReturnStatus,
                //    ViewModel = sysLogMaintenanceViewModel,
                //    ValidationErrors = sysLogMaintenanceViewModel.ValidationErrors,
                //    //MessageBoxView = Helpers.MvcHelpers.RenderPartialView(this, "_MessageBox", sysLogMaintenanceViewModel),
                //    JsonRequestBehavior.AllowGet
                //});

                return Json(1, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
        
    }
}