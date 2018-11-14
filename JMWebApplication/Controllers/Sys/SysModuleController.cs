using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMApplicationService;
using JMCommon;
using JMModels;
using JMWebApplication.Controllers;
using JMDataServiceInterface;
using JMCore;
using JMApplication.ViewModels.Manage;
using JMApplication.Helpers;
using JMApplication.Filters;

namespace JMApplication.Controllers
{
    public class SysModuleController : BaseController
    {        
        ISysModuleDataService sysModuleDataService;
        ISysModuleOperateDataService sysModuleOperateDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysModuleController(ISysModuleDataService dataService,ISysModuleOperateDataService dataService2)
        {
            sysModuleDataService = dataService;
            sysModuleOperateDataService = dataService2;
        }

        ValidationErrors errors = new ValidationErrors();

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>视图</returns>
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();

        }
         

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">分页</param>
        /// <param name="queryStr">查询条件</param>
        /// <returns></returns>
        [SupportFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetList(string id)
        {
            TransactionalInformation transaction;
            SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);

            if (id == null) id = "0";

            List<SysModule> list = sysModuleApplicationService.GetSysModuleList(id, out transaction);
            List<Object> result = new List<Object>();

            foreach (SysModule r in list)
            {
                if (r.ParentId.Equals("0"))
                {
                    result.Add(new
                    {
                        id = r.Id,
                        name = r.Name,
                        EnglishName = r.EnglishName,
                        ParentId = r.ParentId,
                        Url = r.Url,
                        Iconic = r.Iconic,
                        Sort = r.Sort,
                        Remark = r.Remark,
                        Enable = r.Enable,
                        CreatePerson = r.CreatePerson,
                        CreateTime = r.CreateTime,
                        IsLast = r.IsLast,                        
                        State = (sysModuleApplicationService.GetSysModuleList(r.Id, out transaction).Count > 0) ? "closed" : "open"

                    });
                }
                else
                {
                    result.Add(new
                    {
                        id = r.Id,
                        name = r.Name,
                        EnglishName = r.EnglishName,
                        ParentId =r.ParentId,
                        Url = r.Url,
                        Iconic = r.Iconic,
                        Sort = r.Sort,
                        Remark = r.Remark,
                        Enable = r.Enable,
                        CreatePerson = r.CreatePerson,
                        CreateTime = r.CreateTime,
                        IsLast = r.IsLast,
                        _parentId = r.ParentId,
                        State = (sysModuleApplicationService.GetSysModuleList(r.Id, out transaction).Count > 0) ? "closed" : "open"

                    });
                }
            }
            
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", list.Count);
            json.Add("rows", result);

            return Json(json);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetOptListByModule(string mid)
        {
            TransactionalInformation transaction;
            SysModuleOperateApplicationService sysModuleOperateApplicationService = new SysModuleOperateApplicationService(sysModuleOperateDataService);
                      
            List<SysModuleOperate> list = sysModuleOperateApplicationService.GetList(out  transaction,  mid);
            var json = new
            {
                //total = paging.TotalRows,
                rows = (from r in list
                        select new SysModuleOperate()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            ModuleId = r.ModuleId,
                            IsValid = r.IsValid,
                            Sort = r.Sort

                        }).ToArray()

            };

            return Json(json);
        }

        //[HttpPost]
        //[SupportFilter(ActionName = "Index")]
        //public JsonResult GetOptListByModule(DataGridPagingInformation paging, string mid)
        //{
        //    SysModuleOperateApplicationService sysModuleOperateApplicationService = new SysModuleOperateApplicationService(sysModuleOperateDataService);

        //    paging.TotalRows = 1000;
        //    paging.TotalPages = 1;
        //    List<SysModuleOperate> list = sysModuleOperateApplicationService.GetList(ref paging, mid);
        //    var json = new
        //    {
        //        total = paging.TotalRows,
        //        rows = (from r in list
        //                select new SysModuleOperate()
        //                {
        //                    Id = r.Id,
        //                    Name = r.Name,
        //                    KeyCode = r.KeyCode,
        //                    ModuleId = r.ModuleId,
        //                    IsValid = r.IsValid,
        //                    Sort = r.Sort

        //                }).ToArray()

        //    };

        //    return Json(json);
        //}


        #region 创建模块
        [SupportFilter]
        //[SupportFilter(ActionName = "Create")]
        public ActionResult Create(string parentid)
        {
            ViewBag.Perm = GetPermission();

            SysModule sysModule = new SysModule()

            {
                Id= "输入Id",
                ParentId = parentid,
                Enable = true,
                Sort = 0

            };
            return View(sysModule);
        }

        [HttpPost]
        //[SupportFilter(ActionName = "Create")]
         
        public JsonResult Create(FormCollection postedFormData, SysModuleMaintenanceDTO sysModuleDTO)
        {
            TransactionalInformation transaction;
            SysModuleMaintenanceViewModel sysModuleMainteranceViewModel = new SysModuleMaintenanceViewModel();
            SysModule sysModule = new SysModule();

            sysModuleDTO.CreatePerson = GetUserId();
            sysModuleDTO.EnglishName = sysModuleDTO.Name;
            sysModuleDTO.State = "open";

            ModelStateHelper.UpdateViewModel(sysModuleDTO, sysModule);
            
            SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);
            sysModuleApplicationService.CreateSysModule(sysModule, out transaction);

            sysModuleMainteranceViewModel.SysModule = sysModule;
            sysModuleMainteranceViewModel.ReturnStatus = transaction.ReturnStatus;
            sysModuleMainteranceViewModel.ReturnMessage = transaction.ReturnMessage;
            sysModuleMainteranceViewModel.ValidationErrors = transaction.ValidationErrors;
            
            if (transaction.ReturnStatus == false)
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "Id" + sysModule.Id + ",Name" + sysModule.Name + "," + ErrorCol, "失败", "创建", "SysModule");
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
                              
            }
            else
            {
                LogHandler.WriteServiceLog(GetUserId(), "Id" + sysModule.Id + ",Name" + sysModule.Name, "成功", "创建", "SysModule");
                return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
            }
                        
        }
        #endregion


        #region 创建
        [SupportFilter(ActionName = "Create")]
        public ActionResult CreateOpt(string moduleId)
        {            
            ViewBag.Perm = GetPermission();
            SysModuleOperate sysModuleOpt = new SysModuleOperate();

            sysModuleOpt.ModuleId = moduleId;
            sysModuleOpt.IsValid = true;
            return View(sysModuleOpt);

        }

        
        [HttpPost]
        //[SupportFilter(ActionName = "Create")]
        public JsonResult CreateOpt(FormCollection postedFormData, SysModuleOperateMaintenanceDTO sysModuleOperateDTO)
        {
            TransactionalInformation transaction;
            SysModuleOperateMaintenancViewModel sysModuleOperateMaintenancViewModel = new SysModuleOperateMaintenancViewModel();

             
        //if (!ModelState.IsValid)
        //{
        //    var errors = ModelState.errors();
        //sysModuleOperateMaintenancViewModel.ReturnMessage = ModelStateHelper.ReturnErrorMessages(errors);
        //    sysModuleOperateMaintenancViewModel.ValidationErrors = ModelStateHelper.ReturnValidationErrors(errors);
        //    sysModuleOperateMaintenancViewModel.ReturnStatus = false;
        //    var badresponse = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
        //    return badresponse;

        //}
            SysModuleOperate sysModuleOperate = new SysModuleOperate();
            
            ModelStateHelper.UpdateViewModel(sysModuleOperateDTO, sysModuleOperate);

            sysModuleOperate.Id = sysModuleOperate.ModuleId + sysModuleOperate.KeyCode;

            SysModuleOperateApplicationService sysModuleOperateApplicationService = new SysModuleOperateApplicationService(sysModuleOperateDataService);
            sysModuleOperateApplicationService.CreateSysModuleOperate(sysModuleOperate, out transaction);

            sysModuleOperateMaintenancViewModel.SysModuleOperate = sysModuleOperate;
            sysModuleOperateMaintenancViewModel.ReturnStatus = transaction.ReturnStatus;
            sysModuleOperateMaintenancViewModel.ReturnMessage = transaction.ReturnMessage;
            sysModuleOperateMaintenancViewModel.ValidationErrors = transaction.ValidationErrors;

            if (transaction.ReturnStatus == false)
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "Id" + sysModuleOperate.Id + ",Name" + sysModuleOperate.Name + "," + ErrorCol, "失败", "创建", "SysModule");
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
            }
            else
            {
                LogHandler.WriteServiceLog(GetUserId(), "Id" + sysModuleOperate.Id + ",Name" + sysModuleOperate.Name, "成功", "创建", "SysModule");
                return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 修改模块
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            TransactionalInformation transaction;          
            SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);
            ViewBag.Perm = GetPermission();
            SysModule sysModule = sysModuleApplicationService.GetSysModuleBySysModuleID(id, out transaction);
            return View(sysModule);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult Edit(FormCollection postedFormData, SysModuleMaintenanceDTO sysModuleDTO)
        {
            TransactionalInformation transaction;
            SysModuleMaintenanceViewModel sysModuleMainteranceViewModel = new SysModuleMaintenanceViewModel();

            SysModule sysModule = new SysModule();

            sysModuleDTO.CreatePerson = GetUserId();
            sysModuleDTO.EnglishName = sysModuleDTO.Name;
            sysModuleDTO.State = "open";

            ModelStateHelper.UpdateViewModel(sysModuleDTO, sysModule);

            SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);
            sysModuleApplicationService.UpdateSysModule(sysModule, out transaction);

            sysModuleMainteranceViewModel.SysModule = sysModule;
            sysModuleMainteranceViewModel.ReturnStatus = transaction.ReturnStatus;
            sysModuleMainteranceViewModel.ReturnMessage = transaction.ReturnMessage;
            sysModuleMainteranceViewModel.ValidationErrors = transaction.ValidationErrors;
                        
            if (transaction.ReturnStatus == false)
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "Id" + sysModule.Id + ",Name" + sysModule.Name + "," + ErrorCol, "失败", "修改", "SysModule");
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
            }
            else
            {
                LogHandler.WriteServiceLog(GetUserId(), "Id" + sysModule.Id + ",Name" + sysModule.Name, "成功", "修改", "SysModule");
                return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
            }
           

            //TransactionalInformation transaction;
            //SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);

            //if (model != null && ModelState.IsValid)
            //{
            //    if (sysModuleApplicationService.UpdateSysModule(ref errors, model))
            //    {
            //        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "系统菜单");
            //        return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
            //    }
            //    else
            //    {
            //        string ErrorCol = errors.Error;
            //        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "系统菜单");
            //        return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
            //    }
            //}
            //else
            //{
            //    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            //}
        }
        #endregion



        //#region 删除
        //[HttpPost]
        //[SupportFilter]
        //public JsonResult Delete(string id)
        //{
        //    TransactionalInformation transaction;
        //    SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);

        //    if (!string.IsNullOrWhiteSpace(id))
        //    {                           
        //        sysModuleApplicationService.(sysModule, out transaction);

        //        if (sysModuleApplicationService.DeleteSysModuleById(ref errors, id))
        //        {
        //            LogHandler.WriteServiceLog(GetUserId(), "Ids:" + id, "成功", "删除", "模块设置");
        //            return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            string ErrorCol = errors.Error;
        //            LogHandler.WriteServiceLog(GetUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置");
        //            return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
        //    }


        //}


        //[HttpPost]
        //[SupportFilter(ActionName = "Delete")]
        //public JsonResult DeleteOpt(string id)
        //{
        //    SysModuleOperateApplicationService sysModuleOperateApplicationService = new SysModuleOperateApplicationService(sysModuleOperateDataService);

        //    if (!string.IsNullOrWhiteSpace(id))
        //    {
        //        if (sysModuleOperateApplicationService.DeleteSysModuleOperate(ref errors, id))
        //        {
        //            LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "模块设置KeyCode");
        //            return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            string ErrorCol = errors.Error;
        //            LogHandler.WriteServiceLog(GetUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置KeyCode");
        //            return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
        //    }


        //}

        //#endregion
    }
}

