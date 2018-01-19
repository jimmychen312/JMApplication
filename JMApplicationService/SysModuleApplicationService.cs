using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMCommon;

namespace JMApplicationService
{
    public class SysModuleApplicationService
    {
        ISysModuleDataService _sysModuleDataService;

        private ISysModuleDataService SysModuleDataService
        {
            get { return _sysModuleDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysModuleApplicationService(ISysModuleDataService dataService)
        {
            _sysModuleDataService = dataService;
        }


        /// <summary>
        /// Get SysModules By parentId
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<SysModule> GetSysModuleList(string parentId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                SysModuleDataService.CreateSession();
                List<SysModule> sysModules = SysModuleDataService.GetSysModuleList(parentId);
                transaction.ReturnStatus = true;
                return sysModules;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
                return null;
            }
            finally
            {
                SysModuleDataService.CloseSession();
            }

        }

        ///// <summary>
        ///// Get SysModules By parentId
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public List<SysModule> GetSysModuleList(string parentId, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    try
        //    {
        //        SysModuleDataService.CreateSession();
        //        IQueryable<SysModule> queryData = null;
        //        queryData = SysModuleDataService.GetSysModuleList().Where(a => a.ParentId == parentId).OrderBy(a => a.Sort);

        //        transaction.ReturnStatus = true;
        //        return CreateSysModelList(ref queryData);
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);
        //        return null;
        //    }
        //    finally
        //    {
        //        SysModuleDataService.CloseSession();
        //    }


        //}

        ///// <summary>
        ///// 填充modelList
        ///// </summary>
        ///// <param name="queryData"></param>
        ///// <returns></returns>
        //private List<SysModule> CreateSysModelList(ref IQueryable<SysModule> queryData)
        //{
        //    List<SysModule> modelList = (from r in queryData.ToList()
        //                                 select new SysModule
        //                                 {
        //                                     Id = r.Id,
        //                                     Name = r.Name,
        //                                     EnglishName = r.EnglishName,
        //                                     ParentId = r.ParentId,
        //                                     Url = r.Url,
        //                                     Iconic = r.Iconic,
        //                                     Sort = r.Sort,
        //                                     Remark = r.Remark,
        //                                     Enable = r.Enable,
        //                                     CreatePerson = r.CreatePerson,
        //                                     CreateTime = r.CreateTime,
        //                                     IsLast = r.IsLast
        //                                 }).ToList();
        //    return modelList;
        //}


        ///// <summary>
        ///// Get SysModules By System
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public List<SysModule> GetSysModuleBySystem(string parentId,out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    try
        //    {
        //        SysModuleDataService.CreateSession();
        //        List<SysModule> sysModule = SysModuleDataService.GetSysModuleBySystem(parentId).ToList();
        //        transaction.ReturnStatus = true;
        //        return sysModule;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);
        //        return null;
        //    }
        //    finally
        //    {
        //        SysModuleDataService.CloseSession();
        //    }

        //    //return SysModuleDataService.GetSysModuleBySystem(parentId).ToList();
        //}


        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="sysModule"></param>
        /// <param name="transaction"></param>
        public void CreateSysModule(SysModule sysModule, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            //CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();

            try
            {
                SysModuleDataService.CreateSession();

                //customerBusinessRules.ValidateCustomer(customer, CustomerDataService);

                //if (customerBusinessRules.ValidationStatus == true)
                //{
                SysModuleDataService.BeginTransaction();
                SysModuleDataService.CreateSysModule(sysModule);
                SysModuleDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Customer successfully created at " + sysModule.CreateTime.ToString());
                //}
                //else
                //{
                //    transaction.ReturnStatus = customerBusinessRules.ValidationStatus;
                //    transaction.ReturnMessage = customerBusinessRules.ValidationMessage;
                //    transaction.ValidationErrors = customerBusinessRules.ValidationErrors;
                //}

            }
            catch (Exception ex)
            {
                SysModuleDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysModuleDataService.CloseSession();
            }

        }

        //public bool CreateSysModule(ref ValidationErrors errors, SysModule model, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    try
        //    {
        //        SysModuleDataService.CreateSession();
        //        SysModule entity = SysModuleDataService.GetSysModuleById(model.Id);
        //        if (entity != null)
        //        {
        //            errors.Add(Suggestion.PrimaryRepeat);
        //            return false;
        //        }
        //        entity = new SysModule();
        //        entity.Id = model.Id;
        //        entity.Name = model.Name;
        //        entity.EnglishName = model.EnglishName;
        //        entity.ParentId = model.ParentId;
        //        entity.Url = model.Url;
        //        entity.Iconic = model.Iconic;
        //        entity.Sort = model.Sort;
        //        entity.Remark = model.Remark;
        //        entity.Enable = model.Enable;
        //        entity.CreatePerson = model.CreatePerson;
        //        entity.CreateTime = model.CreateTime;
        //        entity.IsLast = model.IsLast;
        //        if (SysModuleDataService.CreateSysModule(entity) == 1)
        //        {
        //            ////分配给角色
        //            //SysModuleDataService.InsertSysRight();
        //            return true;
        //        }
        //        else
        //        {
        //            errors.Add(Suggestion.InsertFail);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);
        //        //return null;

        //        errors.Add(ex.Message);
        //        //ExceptionHander.WriteException(ex);
        //        return false;
        //    }
        //    finally
        //    {
        //        SysModuleDataService.CloseSession();
        //    }


        //}
        //public bool DeleteSysModuleById(ref ValidationErrors errors, string id)
        //{
        //    try
        //    {
        //        SysModuleDataService.CreateSession();
        //        //检查是否有下级
        //        if (SysModuleDataService.GetSysModuleList().AsQueryable().Where(a => a.Id == id).Count() > 0)
        //        {
        //            errors.Add("有下属关联，请先删除下属！");
        //            return false;
        //        }

        //        SysModuleDataService.DeleteSysModuleById(id);
        //        //SysModuleDataService.CommitTransaction(true);

        //        //if (DeleteSysModuleById..SaveChanges() > 0)
        //        //{
        //        //清理无用的项
        //        SysModuleDataService.ClearUnusedRightOperate();

        //            return true;
        //        //}
        //        //else
        //        //{
        //            //return false;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //        //ExceptionHander.WriteException(ex);
        //        return false;
        //    }
        //}

        //public bool UpdateSysModule(ref ValidationErrors errors, SysModule model)
        //{
        //    try
        //    {
        //        SysModule entity = SysModuleDataService.GetSysModuleById(model.Id);
        //        if (entity == null)
        //        {
        //            errors.Add(Suggestion.Disable);
        //            return false;
        //        }
        //        entity.Name = model.Name;
        //        entity.EnglishName = model.EnglishName;
        //        entity.ParentId = model.ParentId;
        //        entity.Url = model.Url;
        //        entity.Iconic = model.Iconic;
        //        entity.Sort = model.Sort;
        //        entity.Remark = model.Remark;
        //        entity.Enable = model.Enable;
        //        entity.IsLast = model.IsLast;

        //        if (SysModuleDataService.UpdateSysModule(entity) == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            errors.Add(Suggestion.EditFail);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //        //ExceptionHander.WriteException(ex);
        //        return false;
        //    }
        //}

        //public SysModule GetById(string id)
        //{
        //    if (IsExist(id))
        //    {
        //        SysModule entity = SysModuleDataService.GetSysModuleById(id);
        //        SysModule model = new SysModule();
        //        model.Id = entity.Id;
        //        model.Name = entity.Name;
        //        model.EnglishName = entity.EnglishName;
        //        model.ParentId = entity.ParentId;
        //        model.Url = entity.Url;
        //        model.Iconic = entity.Iconic;
        //        model.Sort = entity.Sort;
        //        model.Remark = entity.Remark;
        //        model.Enable = entity.Enable;
        //        model.CreatePerson = entity.CreatePerson;
        //        model.CreateTime = entity.CreateTime;
        //        model.IsLast = entity.IsLast;
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public bool IsExist(string id)
        //{
        //    return SysModuleDataService.IsExist(id);
        //}
    }
    
}

     



