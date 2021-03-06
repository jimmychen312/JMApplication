﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;

namespace JMDataServiceInterface
{
    public interface IDataService
    {
        void CreateSession();
        void BeginTransaction();
        void CommitTransaction(Boolean closeSession);
        void RollbackTransaction(Boolean closeSession);
        void CloseSession();
    }

    public interface ICustomerDataService : IDataService, IDisposable
    {
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer GetCustomerByCustomerID(Guid customerID);
        List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging);
        List<PaymentType> GetPaymentTypes();
        PaymentType GetPaymentType(Guid paymentTypeID);
        void DeleteAllCustomers();

    }
    
    public interface IAccountDataService : IDataService, IDisposable
    {
        SysUser Login(string username, string pwd);
    }

    /// <summary>
    /// 取角色模块的操作权限，用于权限控制
    /// </summary>
    public interface ISysRightDataService : IDataService, IDisposable
    {
        List<Permission> GetPermissions(string accountId,string controller);    
    }
    
    //SysSample
    public interface ISysSampleDataService : IDataService, IDisposable
    {
        void CreateSysSample(SysSample sysSample);
        void UpdateSysSample(SysSample sysSample);
        SysSample GetSysSampleById(string Id);
        List<SysSampleInquiry> SysSampleInquiry(string queryStr, DataGridPagingInformation paging);
        void DeleteSysSampleById(string Id);
    }

    //管理树菜单接口
    public interface IManageDataService : IDataService, IDisposable
    {        
        List<SysModuleInquiry> GetMenuByPersonId(string personId, string sysModuleId);
        //List<SysModuleInquiry> GetSysModuleList();
        //int CreateSysModule(SysModule sysModule);
        //void DeleteSysModuleById(string Id);  
        //void UpdateModule(SysModule sysModule);
        //SysModule GetById(string Id);
        //bool IsExist(string Id);
    }


    /// <summary>
    /// test
    /// </summary>
    public interface IPaymentTypeDataService : IDataService, IDisposable
    {
        List<PaymentType> GetPaymentTypes();
    }

    //权限管理接口EF
    public interface ISysModuleDataService : IDataService, IDisposable
    {
        //IQueryable<SysModule> GetSysModuleList();
        List<SysModule> GetSysModuleList( string parentId);

        //List<SysModule> GetSysModuleBySystem(string parentId);
         void CreateSysModule(SysModule sysModule);
        //void DeleteSysModuleById(string Id);
        int UpdateSysModule(SysModule sysModule);
        SysModule GetSysModuleById(string Id);
        //bool IsExist(string Id);
        void InsertSysRight();
        //void ClearUnusedRightOperate();
    }
       
    public interface ISysModuleOperateDataService : IDataService, IDisposable
    {
        //IQueryable<SysModuleOperate> GetModuleOperateList();
        List<SysModuleOperate> GetModuleOperateList();
        void CreateSysModuleOperate(SysModuleOperate sysModuleOperate);
        int DeleteSysModuleOperate(string id);
        SysModuleOperate GetSysModuleOperateById(string id);
        bool IsExist(string id);
    }
    
    //系统日志
    public interface ISysLogDataService : IDataService, IDisposable
    {
        void CreateSysLog(SysLog SysLog);       
        SysLog GetSysLogById(string Id);
        List<SysLogInquiry> SysLogInquiry(string queryStr, DataGridPagingInformation paging);       
        void DeleteSysLogById(string Id);
    }


    //系统异常
    public interface ISysExceptionDataService : IDataService, IDisposable
    {
        void CreateSysException(SysException SysException);
        SysException GetSysExceptionById(string Id);
        List<SysExceptionInquiry> SysExceptionInquiry(string queryStr, DataGridPagingInformation paging);
        void DeleteSysExceptionById(string Id);
    }



}
