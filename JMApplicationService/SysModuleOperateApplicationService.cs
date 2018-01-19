using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Practices.Unity;
using JMModels;
using JMCommon;
//using System.Transactions;
using JMDataServiceInterface;
 
namespace JMApplicationService
{
    public class SysModuleOperateApplicationService
    {
        //[Dependency]
        //public ISysModuleOperateRepository m_Rep { get; set; }

        ISysModuleOperateDataService _sysModuleOperateDataService;

        private ISysModuleOperateDataService SysModuleOperateDataService
        {
            get { return _sysModuleOperateDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysModuleOperateApplicationService(ISysModuleOperateDataService dataService)
        {
            _sysModuleOperateDataService = dataService;
        }


        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<SysModuleOperate> GetList(out TransactionalInformation transaction, string mid)
        {
            transaction = new TransactionalInformation();
            try
            {
                SysModuleOperateDataService.CreateSession();
                List<SysModuleOperate> sysModuleOperate = SysModuleOperateDataService.GetModuleOperateList();

                var sysModuleOperates = sysModuleOperate.Where(x => x.ModuleId == mid).ToList();
                                   
                transaction.ReturnStatus = true;
                return sysModuleOperates;
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
                SysModuleOperateDataService.CloseSession();
            }

        }


        //public List<SysModuleOperate> GetList( string mid)
        //{
        //    List<SysModuleOperate> queryData = SysModuleOperateDataService.GetModuleOperateList().Where(a => a.ModuleId == mid);
        //    //paging.TotalRows = queryData.Count();
        //    //queryData = LinqHelper.SortingAndPaging(queryData, paging.SortExpression, paging.SortDirection, paging.TotalPages, paging.TotalRows);
        //    return CreateModelList(ref queryData);
        //}

        //private List<SysModuleOperate> CreateModelList(ref IQueryable<SysModuleOperate> queryData)
        //{

        //    List<SysModuleOperate> modelList = (from r in queryData
        //                                        select new SysModuleOperate
        //                                        {
        //                                            Id = r.Id,
        //                                            Name = r.Name,
        //                                            KeyCode = r.KeyCode,
        //                                            ModuleId = r.ModuleId,
        //                                            IsValid = r.IsValid,
        //                                            Sort = r.Sort
        //                                        }).ToList();
        //    return modelList;
        //}

        //public List<SysModuleOperate> GetList(ref DataGridPagingInformation paging, string mid)
        //{
        //    IQueryable<SysModuleOperate> queryData = SysModuleOperateDataService.GetModuleOperateList().Where(a => a.ModuleId == mid);
        //    paging.TotalRows = queryData.Count();
        //    queryData = LinqHelper.SortingAndPaging(queryData, paging.SortExpression, paging.SortDirection, paging.TotalPages, paging.TotalRows);
        //    return CreateModelList(ref queryData);
        //}

        //private List<SysModuleOperate> CreateModelList(ref IQueryable<SysModuleOperate> queryData)
        //{

        //    List<SysModuleOperate> modelList = (from r in queryData
        //                                             select new SysModuleOperate
        //                                             {
        //                                                 Id = r.Id,
        //                                                 Name = r.Name,
        //                                                 KeyCode = r.KeyCode,
        //                                                 ModuleId = r.ModuleId,
        //                                                 IsValid = r.IsValid,
        //                                                 Sort = r.Sort
        //                                             }).ToList();
        //    return modelList;
        //}

        public bool CreateSysModuleOperate(ref ValidationErrors errors, SysModuleOperate model)
        {
            try
            {
                SysModuleOperate entity = SysModuleOperateDataService.GetSysModuleOperateById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysModuleOperate();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.KeyCode = model.KeyCode;
                entity.ModuleId = model.ModuleId;
                entity.IsValid = model.IsValid;
                entity.Sort = model.Sort;
                if (SysModuleOperateDataService.CreateSysModuleOperate(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                //ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool DeleteSysModuleOperate(ref ValidationErrors errors, string id)
        {
            try
            {
                if (SysModuleOperateDataService.DeleteSysModuleOperate(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                //ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool IsExists(string id)
        {
            SysModuleOperate sysModuleOperate = SysModuleOperateDataService.GetSysModuleOperateById(id);
            
            if (sysModuleOperate != null)
            {
                return true;
            }
            return false;
        }

        public SysModuleOperate GetSysModuleOperateById(string id)
        {
            if (IsExist(id))
            {
                SysModuleOperate entity = SysModuleOperateDataService.GetSysModuleOperateById(id);
                SysModuleOperate model = new SysModuleOperate();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.KeyCode = entity.KeyCode;
                model.ModuleId = entity.ModuleId;
                model.IsValid = entity.IsValid;
                model.Sort = entity.Sort;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return SysModuleOperateDataService.IsExist(id);
        }
    }
}

