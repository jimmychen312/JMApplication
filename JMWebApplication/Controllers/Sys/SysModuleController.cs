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

namespace JMApplication.Controllers
{
    public class SysModuleController : BaseController
    {
        ///// <summary>
        ///// 业务层注入
        ///// </summary>
        //[Dependency]
        //public ISysModuleBLL m_BLL { get; set; }
        //[Dependency]
        //public ISysModuleOperateBLL operateBLL { get; set; }

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
        //[SupportFilter]
        [SupportFilter(ActionName = "Create")]
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();
            SysModule entity = new SysModule()
            {
                ParentId = id,
                Enable = true,
                Sort = 0
            };
            return View(entity);
        }

        [HttpPost]
        //[SupportFilter(ActionName = "Create")]
         
        public JsonResult Create(FormCollection postedFormData, [System.Web.Http.FromBody] SysModuleMaintenanceDTO sysModuleDTO)
        {
            TransactionalInformation transaction;
            SysModuleMaintenanceViewModel sysModuleMainteranceViewModel = new SysModuleMaintenanceViewModel();
            SysModule sysModule = new SysModule();
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


        //#region 创建
        //[SupportFilter(ActionName = "Create")]
        //public ActionResult CreateOpt(string moduleId)
        //{
        //    //TransactionalInformation transaction;
        //    SysModuleOperateApplicationService sysModuleOperateApplicationService = new SysModuleOperateApplicationService(sysModuleOperateDataService);

        //    ViewBag.Perm = GetPermission();
        //    SysModuleOperate sysModuleOpt = new SysModuleOperate();
        //    sysModuleOpt.ModuleId = moduleId;
        //    sysModuleOpt.IsValid = true;
        //    return View(sysModuleOpt);
        //}


        //[HttpPost]
        //[SupportFilter(ActionName = "Create")]
        //public JsonResult CreateOpt(SysModuleOperate info)
        //{
        //    SysModuleOperateApplicationService sysModuleOperateApplicationService = new SysModuleOperateApplicationService(sysModuleOperateDataService);

        //    if (info != null && ModelState.IsValid)
        //    {
        //        SysModuleOperate entity = sysModuleOperateApplicationService.GetSysModuleOperateById(info.Id);
        //        if (entity != null)
        //            return Json(JsonHandler.CreateMessage(0, Suggestion.PrimaryRepeat), JsonRequestBehavior.AllowGet);
        //        entity = new SysModuleOperate();
        //        entity.Id = info.ModuleId + info.KeyCode;
        //        entity.Name = info.Name;
        //        entity.KeyCode = info.KeyCode;
        //        entity.ModuleId = info.ModuleId;
        //        entity.IsValid = info.IsValid;
        //        entity.Sort = info.Sort;

        //        if (sysModuleOperateApplicationService.CreateSysModuleOperate(ref errors, entity))
        //        {
        //            LogHandler.WriteServiceLog(GetUserId(), "Id:" + info.Id + ",Name:" + info.Name, "成功", "创建", "模块设置");
        //            return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            string ErrorCol = errors.Error;
        //            LogHandler.WriteServiceLog(GetUserId(), "Id:" + info.Id + ",Name:" + info.Name + "," + ErrorCol, "失败", "创建", "模块设置");
        //            return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail), JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

        //#region 修改模块
        //[SupportFilter]
        //public ActionResult Edit(string id)
        //{
        //    SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);
        //    ViewBag.Perm = GetPermission();
        //    SysModule entity = sysModuleApplicationService.GetById(id);
        //    return View(entity);
        //}

        //[HttpPost]
        //[SupportFilter]
        //public JsonResult Edit(SysModule model)
        //{
        //    SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);

        //    if (model != null && ModelState.IsValid)
        //    {
        //        if (sysModuleApplicationService.UpdateSysModule(ref errors, model))
        //        {
        //            LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "系统菜单");
        //            return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
        //        }
        //        else
        //        {
        //            string ErrorCol = errors.Error;
        //            LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "系统菜单");
        //            return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
        //        }
        //    }
        //    else
        //    {
        //        return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
        //    }
        //}
        //#endregion



        //#region 删除
        //[HttpPost]
        //[SupportFilter]
        //public JsonResult Delete(string id)
        //{
        //    SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);

        //    if (!string.IsNullOrWhiteSpace(id))
        //    {
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

