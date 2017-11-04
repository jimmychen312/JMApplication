using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using JM.ViewModels.Manage;
using JMApplicationService;
using JMDataServiceInterface;
using JMModels;

namespace JMApplication.Controllers
{
    public class SysLogController : Controller
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
        /// 

        public JsonResult GetList()
        {
            TransactionalInformation transaction;

            SysLogInquiryViewModel sysLogInquiryViewModel = new SysLogInquiryViewModel();

            
            SysLogApplicationService sysLogApplicationService = new SysLogApplicationService(sysLogDataService);

            List<SysLogList> sysLogs = sysLogApplicationService.GetSysLogList( out transaction);

            //if (id != string.Empty)
            //{
                sysLogInquiryViewModel.SysLogLists = sysLogs;
                sysLogInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
                sysLogInquiryViewModel.ReturnMessage = transaction.ReturnMessage;

                var json = new
                {
                    // total = pager.totalRows,
                    rows = (from m in sysLogInquiryViewModel.SysLogLists
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


        ////
        //// GET: /SysLog/
        //[Dependency]
        //public ISysLogBLL logBLL { get; set; }



        public ActionResult Index()
        {

            return View();

        }

        //public JsonResult GetList(GridPager pager, string queryStr)
        //{
        //    List<SysLog> list = logBLL.GetList(ref pager, queryStr);
        //    var json = new
        //    {
        //        total = pager.totalRows,
        //        rows = (from r in list
        //                select new SysLogModel()
        //                {

        //                    Id = r.Id,
        //                    Operator = r.Operator,
        //                    Message = r.Message,
        //                    Result = r.Result,
        //                    Type = r.Type,
        //                    Module = r.Module,
        //                    CreateTime = r.CreateTime

        //                }).ToArray()

        //    };

        //    return Json(json);
        //}


        //#region 详细

        //public ActionResult Details(string id)
        //{

        //    SysLog entity = logBLL.GetById(id);
        //    SysLogModel info = new SysLogModel()
        //    {
        //        Id = entity.Id,
        //        Operator = entity.Operator,
        //        Message = entity.Message,
        //        Result = entity.Result,
        //        Type = entity.Type,
        //        Module = entity.Module,
        //        CreateTime = entity.CreateTime,
        //    };
        //    return View(info);
        //}

        //#endregion
    }
}